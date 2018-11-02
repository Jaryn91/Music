var songApi = getDomain() + "api/songs/";
var allSongs;


function getAllSongs(lstBoxName, func) {
    $.ajax({
        url: songApi,
        headers: getAuthorizationHeader(),
        success: function (result) {
            func(lstBoxName, result);
            allSongs = result;
        },
        error: function (result, status) {
            errorHandling(result, null);
        }
    });
}

function addOptionsToListBox(lstBoxName, result) {
    var options = "";
    var songs = result.songs;
    for (var i = 0; i < songs.length; i++) {
        options += createOption(songs[i]);
    }
    $(lstBoxName).append(options);
}


function createOption(result) {
    var option = '<li class="ui-state-default" value="' + result.id + '">' + result.name + "</li>";
    return option;
}

function filter(text) {
    if (text === "")
        return allSongs;
    var filteredSongs = new Array();
    for (var i = 0; i < allSongs.length; i++) {
        if (allSongs[i].name.toLowerCase().indexOf(text) >= 0) {
            filteredSongs.push(allSongs[i]);
        }
    }
    return filteredSongs;
}

function populateListBox(lsbBoxName, songs) {
    $(lsbBoxName).empty();
    addOptionsToListBox(lsbBoxName, songs);
}