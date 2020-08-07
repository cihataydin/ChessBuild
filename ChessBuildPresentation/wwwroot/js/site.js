// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var uri = "ws://" + window.location.host + "/ws";
var counter = 0;

function connect() {
    socket = new WebSocket(uri);
    
    socket.onopen = function (event) {
        var id = event.data;
        if (id.length == 36) {
            window.sessionStorage.setItem("connectionId", id);
        }
        console.log("opened connection to " + uri);
    };
    socket.onclose = function (event) {
        console.log("closed connection from " + uri);
    };
    socket.onmessage = function (event) {
        messageArray = event.data.split(" ", 2);
        conId = window.sessionStorage.getItem("connectionId");

        if (messageArray[1] == conId) {
            movePiece(messageArray[0]);
        }
    };
    socket.onerror = function (event) {
        console.log("error: " + event.data);
    };
}

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
    //console.log(x);
    //console.log(y);

    $.ajax({
        async: true,
        url: '/Home/Click',
        data: { initialX: firstDataX, initialY: firstDataY, instantaneousX: secondDataX, instantaneousY: secondDataY },
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

        $.ajax({
            async: true,
            url: '/Home/Click',
            data: { initialX: firstDataX, initialY: firstDataY, instantaneousX: secondDataX, instantaneousY: secondDataY },
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
        var dataX = $(this).attr("valuex");
        var dataY = $(this).attr("valuey");
        storeData(dataX, dataY);
        var message = useStoredData();

        if (socket != null && message != null) {
            socket.send(message);
        }      
    });
});


