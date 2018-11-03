var songApi = getDomain() + "api/songs/";
var allSongs;


function getAllSongs(lstBoxName, func) {
    $.ajax({
        url: songApi,
        headers: getAuthorizationHeader(),
        success: function (result) {
            allSongs = result.songs;
            func(lstBoxName);
            
        },
        error: function (result, status) {
            errorHandling(result, null);
        }
    });
}

function addOptionsToListBox(lstBoxName) {
    var options = "";
    for (var i = 0; i < allSongs.length; i++) {
        options += createOption(allSongs[i]);
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