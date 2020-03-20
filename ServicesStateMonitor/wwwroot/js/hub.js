"use strict";

var classAllRight = "table-success";
var classAffected = "table-warning";
var classProblem = "table-danger";

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
        elem.className = GetClass(state);
        elem.textContent = state;
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