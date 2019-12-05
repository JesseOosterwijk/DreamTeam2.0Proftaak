// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    var connection = new WebSocketManager.Connection("wss://localhost:44316/chat");
    connection.enableLogging = true;

    connection.connectionMethods.onConnected = () => {

    }

    connection.connectionMethods.onDisconnected = () => {

    }

    connection.clientMethods["pingMessage"] = (socketId, message, title) => {
        var messageT = new Object();
        messageT.Content = message;
        messageT.Title = title;
        messageT.SocketId = socketId;
        var messageText = messageT.SocketId + ' said: ' + messageT.Content + ' at: ';
        $('#messages').append('<li>' + messageText + '</li>')
        $('#messages').scrollTop($('#messages').prop('scrollHeight'));
    }

    connection.start();

    var $messagecontent = $('#message-content');
    var $messagetitle = $('#message-title');
    $messagecontent.keyup(function (e) {
        if (e.keyCode == 13) {
            var message = $messagecontent.val().trim();
            var title = $messagetitle.val().trim();
            if (message.length == 0) {
                return false;
            }
            if (title.length == 0) {
                return false;
            }

            connection.invoke("SendMessage", connection.connectionId, message, title);
            $messagecontent.val('');
        }
    })
    $('#messages').scrollTop($('#messages').prop('scrollHeight'));
});
