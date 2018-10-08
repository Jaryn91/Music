function GetAllSongs(lstBoxName, func) {
    var link = GetDomain();
    var url = link + "api/songs/";
    var accessToken = localStorage.getItem('access_token');
    var headers = { Authorization: 'Bearer ' + accessToken };
    $.ajax({
        url: url,
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
    var link = GetDomain();
    var url = link + "api/songs/";
    var accessToken = localStorage.getItem('access_token');
    var headers = { Authorization: 'Bearer ' + accessToken };

    $.ajax({
        url: url,
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
    var link = GetDomain();
    var url = link + "api/songs/" + songId;
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
    var link = GetDomain();
    var accessToken = localStorage.getItem('access_token');
    var headers = { Authorization: 'Bearer ' + accessToken };
    var url = link + "api/songs/" + songId;
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