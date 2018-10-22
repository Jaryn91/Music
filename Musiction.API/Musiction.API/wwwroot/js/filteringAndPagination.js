var songApi = getDomain() + "api/songs/";
var allSongs;
var page = 0;
var itemsPerPage = 5;

function getAllSongs(lstBoxName, func) {
    $.ajax({
        url: songApi,
        headers: getAuthorizationHeader(),
        success: function (result) {
            func(lstBoxName, result);
        },
        error: function (result, status) {
            errorHandling(result, null);
        }
    });
}

function addOptionsToListBox(lstBoxName, result) {
    var options = '';
    for (var i = 0; i < result.length; i++) {
        options += createOption(result[i]);
    }
    $(lstBoxName).append(options);
    allSongs = result;
}


function createOption(result) {
    var option = '<li class="ui-state-default" value="' + result.id + '">' + result.name + '</li>';
    return option;
}

function filter(text) {
    var filteredSongs = new Array();
    for (var i = 0; i < allSongs.length; i++) {
        if (allSongs[i].name.toLowerCase().indexOf(text) >= 0) {
            filteredSongs.push(allSongs[i]);
        }
    }

}