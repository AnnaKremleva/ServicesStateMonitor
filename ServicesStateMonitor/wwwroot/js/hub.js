"use strict";

var classAllRight = "btn-success";
var classAffected = "btn-warning";
var classProblem = "btn-danger";
var classNeutral = "btn-secondary";

var stateAllRight = "AllRight";
var stateAffected = "AffectedByProblem";
var stateProblem = "HasProblem";

var connection = new signalR.HubConnectionBuilder().withUrl("/servicesHub").build();

connection.start().then(function() {
    connection.invoke("Init");
});

connection.on("Updated",
    function(name, state) {
        const elem = document.getElementById(name);
        RemoveStateClasses(elem);
        elem.classList.add(GetClass(state));
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