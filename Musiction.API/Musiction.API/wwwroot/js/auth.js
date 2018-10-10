function getAuthorizationHeader() {
    var accessToken = localStorage.getItem('access_token');
    var header = { Authorization: 'Bearer ' + accessToken };
    return header;
}