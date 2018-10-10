var presentationsApi = link + "api/presentation/";

function createPresentation(outputFileType, songs, func) {
    var url = presentationsApi + outputFileType + songs;
    $.ajax({
        url: url,
        type: 'GET',
        success: func
    });
}