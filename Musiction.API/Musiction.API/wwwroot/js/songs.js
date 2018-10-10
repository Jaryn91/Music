﻿var songApi = getDomain() + "api/songs/";

function getAllSongs(lstBoxName, func) {
    $.ajax({
        url: songApi,
        headers: getAuthorizationHeader()
    })
        .done(function (result) {
            func(lstBoxName, result);
        });
};



function AddOptionsToListBox(lstBoxName, result) {
    var option = '';
    for (var i = 0; i < result.length; i++) {
        option += '<li class="ui-state-default" value="' + result[i].id + '">' + result[i].name + '</li>';
    }
    $(lstBoxName).append(option);
};


function AddSong(song, func) {
    $.ajax({
        url: songApi,
        type: 'POST',
        data: song,
        headers: getAuthorizationHeader(),
        processData: false, // tell jQuery not to process the data
        contentType: false, // tell jQuery not to set contentType
        success: function (result) {
        },
        error: function (jqXHR) {
        },
        complete: function (jqXHR, status) {
            func();
        }
    });
}

function getSong(songId, func) {
    var url = songApi + songId;
    $.ajax({
        url: url,
        headers: getAuthorizationHeader()
    })
        .done(function (result) {
            func(result);
        });
}

function updateSong(songId, song, func) {
    var url = songApi + songId;
    $.ajax({
        url: url,
        type: "PUT",
        data: song,
        headers: getAuthorizationHeader(),
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (result) {
            func();
        },
        error: function (jqXHR) {
        },
        complete: function (jqXHR, status) {
        }
    });
}

function deleteSong(songId) {
    var url = songApi + songId;
    $.ajax({
        url: url,
        headers: getAuthorizationHeader(),
        type: 'DELETE',
        success: function (result) {
            alert("usunięto");
        }
    });

}