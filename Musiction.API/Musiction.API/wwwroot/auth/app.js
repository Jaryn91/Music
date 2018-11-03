$('document').ready(function () {
    var content = $('.content');
    var loadingSpinner = $('#loading');
    content.css('display', 'block');
    loadingSpinner.css('display', 'none');

    var userProfile;

    var webAuth = new auth0.WebAuth({
        domain: AUTH0_DOMAIN,
        clientID: AUTH0_CLIENT_ID,
        redirectUri: AUTH0_CALLBACK_URL,
        audience: AUTH0_AUDIENCE,
        responseType: 'token id_token',
        scope: 'openid profile read:messages',
        leeway: 60
    });

    var homeView = $('#home-view');
    var profileView = $('#profile-view');
    var pingView = $('#ping-view');

    // buttons and event listeners
    var loginBtn = $('#qsLoginBtn');
    var logoutBtn = $('#qsLogoutBtn');

    var profileViewBtn = $('#btn-profile-view');
    var pingViewBtn = $('#btn-ping-view');


    loginBtn.click(login);
    logoutBtn.click(logout);


    profileViewBtn.click(function () {
        homeView.css('display', 'none');
        profileView.css('display', 'inline-block');
        getProfile();
    });

    pingViewBtn.click(function () {
        testAuthorization();
    });

    function login() {
        webAuth.authorize();

    }

    function setSession(authResult) {
        var expiresAt = JSON.stringify(
            authResult.expiresIn * 1000 + new Date().getTime()
        );
        localStorage.setItem('access_token', authResult.accessToken);
        localStorage.setItem('id_token', authResult.idToken);
        localStorage.setItem('expires_at', expiresAt);
    }

    function logout() {
        localStorage.removeItem('access_token');
        localStorage.removeItem('id_token');
        localStorage.removeItem('expires_at');
        displayButtons();
    }

    function isAuthenticated() {
        var expiresAt = JSON.parse(localStorage.getItem('expires_at'));
        return new Date().getTime() < expiresAt;
    }

    function displayButtons() {
        var loginStatus = $('.container h4');
        if (isAuthenticated()) {
            loginBtn.css('display', 'none');
            logoutBtn.css('display', 'inline-block');
            profileViewBtn.css('display', 'inline-block');
            loginStatus.text(
                'Juhu! Jesteś zalogowany!');
            getRemainingCredits(displayCredits);
        } else {
            homeView.css('display', 'inline-block');
            loginBtn.css('display', 'inline-block');
            logoutBtn.css('display', 'none');
            profileViewBtn.css('display', 'none');
            profileView.css('display', 'none');
            pingView.css('display', 'none');
            loginStatus.text('Zaloguj się proszę :).');
        }
    }

    function getProfile() {
        if (!userProfile) {
            var accessToken = localStorage.getItem('access_token');

            if (!accessToken) {
                console.log('Access token must exist to fetch profile');
            }

            webAuth.client.userInfo(accessToken, function (err, profile) {
                if (profile) {
                    userProfile = profile;
                    displayProfile();
                }
            });
        } else {
            displayProfile();
        }
    }

    function displayProfile() {
        // display the profile
        $('#profile-view .nickname').text(userProfile.nickname);
        $('#profile-view .full-profile').text(JSON.stringify(userProfile, null, 2));
        $('#profile-view img').attr('src', userProfile.picture);
    }

    function displayCredits(credits) {
        var zipsLeft = $('.container h5');
        zipsLeft.text('Do końca miesiąca można jeszcze wygenerować ' + credits + ' zipów.');
    }

    function handleAuthentication() {
        webAuth.parseHash(function (err, authResult) {
            if (authResult && authResult.accessToken && authResult.idToken) {
                window.location.hash = '';
                setSession(authResult);
                loginBtn.css('display', 'none');
                homeView.css('display', 'inline-block');
            } else if (err) {
                homeView.css('display', 'inline-block');
                console.log(err);
                alert(
                    'Error: ' + err.error + '. Check the console for further details.'
                );
            }
            displayButtons();
        });
    }

    handleAuthentication();

    displayButtons();
});