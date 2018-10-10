var songApi = getDomain() + "api/songs/";

function getAllSongs(lstBoxName, func) {

    var accessToken = localStorage.getItem('access_token');
    var headers = { Authorization: 'Bearer ' + accessToken };
    $.ajax({
        url: songApi,
        headers: headers
    })
        .done(function (result) {
            func(lstBoxName, result);
        });
};



function AddOptionsToListBox(lstBoxName, result) {
    var option = '';
    for (var i = 0; i < result.length; i++) {
        option += '<option value="' + result[i].id + '">' + result[i].name + '</option>';
    }
    $(lstBoxName).append(option);
};


function AddSong(song, func) {

    var accessToken = localStorage.getItem('access_token');
    var headers = { Authorization: 'Bearer ' + accessToken };

    $.ajax({
        url: songApi,
        type: 'POST',
        data: song,
        headers: headers,
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
    var accessToken = localStorage.getItem('access_token');
    var headers = { Authorization: 'Bearer ' + accessToken };
    $.ajax({
        url: url,
        headers: headers
    })
        .done(function (result) {
            func(result);
        });
}

function updateSong(songId, song, func) {
    
    var accessToken = localStorage.getItem('access_token');
    var headers = { Authorization: 'Bearer ' + accessToken };
    var url = songApi + songId;
    $.ajax({
        url: url,
        type: "PUT",
        data: song,
        headers: headers,
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
        type: 'DELETE',
        success: function (result) {
            alert("usunięto");
        }
    });

}