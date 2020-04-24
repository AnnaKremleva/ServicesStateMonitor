"use strict";

var classAllRight = "btn-secondary";
var classAffected = "btn-warning";
var classProblem = "btn-danger";
var classNeutral = "btn-secondary";

var stateAllRight = "AllRight";
var stateAffected = "AffectedByProblem";
var stateProblem = "HasProblem";

var connection = new signalR.HubConnectionBuilder().withUrl("/servicesHub").build();
var svg = document.getElementById("linesContainer");

connection.start().then(function() {
    connection.invoke("Init");
});

window.onresize = handleWindowSize;

function handleWindowSize() {
    connection.invoke("UpdateLines");
}

connection.on("Updated",
    function(name, state) {
        const elem = document.getElementById(name);
        RemoveStateClasses(elem);
        elem.classList.add(GetClass(state));
    });

connection.on("LineDraw",
    function(serviceId, dependentId) {
        const service = document.getElementById(serviceId);
        const dependent = document.getElementById(dependentId);
        const lineId = serviceId + dependentId;

        ConnectByLine(service, dependent, lineId);
    });

function GetClass(state) {
    var stateClass = classAllRight;
    if (state === stateAffected) {
        stateClass = classAffected;
    }
    if (state === stateProblem) {
        stateClass = classProblem;
    }
    return stateClass;
}

function RemoveStateClasses(elem) {
    elem.classList.remove(classAllRight);
    elem.classList.remove(classAffected);
    elem.classList.remove(classProblem);
    elem.classList.remove(classNeutral);
}

function ConnectByLine(from, to, lineId) {

    const targetWidth = $(to).width() / 2;
    const targetHeight = $(to).height() / 2;
    const fromWidth = $(from).width() / 2;
    const fromHeight = $(from).height() / 2;

    const fromX = $(from).offset().left + fromWidth;
    const fromY = $(from).offset().top + fromHeight;
    const targetX = $(to).offset().left + targetWidth;
    const targetY = $(to).offset().top + targetHeight;

    const line = document.getElementById(lineId);
    line.setAttribute("x1", fromX);
    line.setAttribute("y1", fromY);
    line.setAttribute("x2", targetX);
    line.setAttribute("y2", targetY);

    const pointerHeight = 3;
    const pointerLength = 75;

    const deltaX = targetX - fromX;
    const deltaY = targetY - fromY;

    const pointerDeltaX = Math.abs(deltaX) > Math.abs(deltaY) ? 0 : pointerHeight;
    const pointerDeltaY = Math.abs(deltaX) > Math.abs(deltaY) ? pointerHeight : 0;

    const pointerCoefficient = Math.abs(deltaX) > Math.abs(deltaY)
        ? 1 + (targetWidth + pointerLength) / (Math.abs(deltaX) - targetWidth)
        : 1 + (targetHeight + pointerLength) / (Math.abs(deltaY) - targetHeight);

    const pointerX1 = targetX;
    const pointerY1 = targetY;

    const pointerX0 = fromX + deltaX / pointerCoefficient;
    const pointerY0 = fromY + deltaY / pointerCoefficient;

    const pointerX2 = pointerX0 - pointerDeltaX;
    const pointerY2 = pointerY0 - pointerDeltaY;

    const pointerX3 = pointerX0 + pointerDeltaX;
    const pointerY3 = pointerY0 + pointerDeltaY;

    let points = pointerX1 + "," + pointerY1 + " " + pointerX2 + "," + pointerY2 + " " + pointerX3 + "," + pointerY3;

    const linePointer = document.getElementById(lineId + "Pointer");
    linePointer.setAttribute("points", points);
}