// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var uri = "ws://" + window.location.host + "/ws";
var counter = 0;
var boardCounter;
var userColor;
var key;
var element = document.getElementById("board");
var black = 0;
var white = 1;

function connect() {
    socket = new WebSocket(uri);

    socket.onopen = function (event) {
        console.log("opened connection to " + uri);

    };
    socket.onclose = function (event) {
        console.log("closed connection from " + uri);
    };
    socket.onmessage = function (event) {
        if (event.data.length == 4) {
            movePiece(event.data);
        }
        if (event.data.length == 23) {
            console.log(event.data);
        }
        if (event.data == " Match is completed....") {
            key = true;
        }
        else if (event.data == "Waiting for opponent...") {
            key = false;
        }

        if (event.data == "Black") {
            element.setAttribute("userColor", "0");
            updateBoard();
        }
        if (event.data == "White") {
            element.setAttribute("userColor", "1");
            updateBoard();
        }
    };
    socket.onerror = function (event) {
        console.log("error: " + event.data);
    };
}

function updateBoard() {
    boardCounter = window.sessionStorage.getItem("attr");
    let userColor = getColor();
    $.ajax({
        async: false,
        url: '/Home/Click',
        data: { initialX: 0, initialY: 0, instantaneousX: 0, instantaneousY: 0, count: boardCounter, color: userColor, onMessage: "Update" },
        method: "POST",
        //contentType: "application/json;charset=utf-8",
        success: function (d) {
            $('body').html(d)
        }, error: function () {
            alert("hata olustu");
        }, complete: function () {
        }
    });
}

function getColor() {
    var color = element.getAttribute("userColor");
    return parseInt(color);
}

function setValue() {
    var element = document.getElementById("board");
    var attr = element.getAttribute("value");
    window.sessionStorage.setItem("attr", attr);
}
setValue();


function movePiece(param) {
    var firstDataX;
    var firstDataY;
    var secondDataX;
    var secondDataY;


    for (var i = 0; i < 4; i++) {
        if (i == 0) firstDataX = parseInt(param.charAt(i));
        if (i == 1) firstDataY = parseInt(param.charAt(i));
        if (i == 2) secondDataX = parseInt(param.charAt(i));
        if (i == 3) secondDataY = parseInt(param.charAt(i));
    }
    boardCounter = window.sessionStorage.getItem("attr");
    userColor = 1 - getColor();

    console.log(boardCounter + " onmessage");
    $.ajax({
        async: false,
        url: '/Home/Click',
        data: { initialX: firstDataX, initialY: firstDataY, instantaneousX: secondDataX, instantaneousY: secondDataY, count: boardCounter, color: userColor, onMessage: "Yes" },
        method: "POST",
        //contentType: "application/json;charset=utf-8",
        success: function (d) {
            $('body').html(d)
        }, error: function () {
            alert("hata olustu");
        }, complete: function () {
        }
    });
}

function storeData(dataX, dataY) {

    if (counter == 0) {
        window.sessionStorage.setItem("firstDataX", dataX);
        window.sessionStorage.setItem("firstDataY", dataY);

    }
    else if (counter == 1) {
        window.sessionStorage.setItem("secondDataX", dataX);
        window.sessionStorage.setItem("secondDataY", dataY);

    }

    counter++;

}

function useStoredData() {
    if (counter == 2) {


        firstDataX = parseInt(window.sessionStorage.getItem("firstDataX"));
        firstDataY = parseInt(window.sessionStorage.getItem("firstDataY"));
        secondDataX = parseInt(window.sessionStorage.getItem("secondDataX"));
        secondDataY = parseInt(window.sessionStorage.getItem("secondDataY"));
        boardCounter = window.sessionStorage.getItem("attr");
        let userColor = getColor();
        console.log(boardCounter + " internal")

        $.ajax({
            async: false,
            url: '/Home/Click',
            data: { initialX: firstDataX, initialY: firstDataY, instantaneousX: secondDataX, instantaneousY: secondDataY, count: boardCounter, color: userColor, onMessage : "No" },
            method: "POST",
            //contentType: "application/json;charset=utf-8",
            success: function (d) {
                $('body').html(d)
            }, error: function () {
                alert("hata olustu");
            }, complete: function () {
            }
        });
        counter = 0;
        var message = firstDataX.toString() + firstDataY.toString() + secondDataX.toString() + secondDataY.toString();
        return message;
    }
}

$(function () {
    $("img").click(function () {
        var userBlackOrWhite = getColor();
        var stoneColor = parseInt($(this).attr("stoneColor"));
        if (key) {
            console.log(stoneColor);
            console.log(userBlackOrWhite)
            var dataX = $(this).attr("valuex");
            var dataY = $(this).attr("valuey");
            storeData(dataX, dataY);
            var message = useStoredData();

            if (socket != null && message != null) {
                socket.send(message);
            }
        }
    });
});


