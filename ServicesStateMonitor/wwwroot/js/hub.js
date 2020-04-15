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
var counter = 0;

connection.start().then(function() {
    counter = 0;
    connection.invoke("Init");
});

window.onresize = handleWindowSize;

function handleWindowSize() {
    //while (svg.firstChild) {
    //    svg.firstChild.remove();
    //}
    counter = 0;
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

        ConnectByLine(service, dependent);
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

function ConnectByLine(from, to) {

    const fromX = $(from).offset().left + ($(to).width() / 2);
    const fromY = $(from).offset().top + ($(from).height() / 2);
    const targetX = $(to).offset().left + ($(to).width() / 2);
    const targetY = $(to).offset().top + ($(to).height() / 2);

    let cTy;
    if (targetY > fromY) {
        cTy = $(to).offset().top;
    } else {
        cTy = $(to).offset().top + $(to).height();
    }

    let line0=document.getElementById("line" + counter);
    line0.setAttribute("x1", fromX);
    line0.setAttribute("y1", fromY);
    line0.setAttribute("x2", targetX);
    line0.setAttribute("y2", cTy);
    line0.setAttribute("style", "stroke: black");

    counter++;
}