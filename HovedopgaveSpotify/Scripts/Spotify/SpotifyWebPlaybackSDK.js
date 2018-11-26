window.onSpotifyWebPlaybackSDKReady = () => {
    const token = access_token;
    const player = new Spotify.Player({
        name: 'Hovedopgave Spotify',
        getOAuthToken: cb => { cb(token); }
    });

    // Error handling
    player.addListener('initialization_error', ({ message }) => { console.error(message); });
    player.addListener('authentication_error', ({ message }) => { console.error(message); });
    player.addListener('account_error', ({ message }) => { console.error(message); });
    player.addListener('playback_error', ({ message }) => { console.error(message); });

    // Playback status updates'
  
    player.addListener('player_state_changed', state => { console.log(state) });

    player.addListener('player_state_changed', ({
  position,
        duration,
        track_window: { current_track }
    }) => {
        console.log('Currently Playing', current_track.artists["0"].name, " - ", current_track.name);
        console.log('Position in Song', position);
        console.log('Duration of Song', duration);
    });
    // Ready
    player.addListener('ready', ({ device_id }) => {
        console.log('Ready with Device ID', device_id);

        $.ajax(
            {
                type: "POST", //HTTP POST Method
                url: "Transfer", // Controller/View
                data: { //Passing data
                    deviceID: device_id, //Reading text box values using Jquery
                    access_token: access_token,
                }
            });
    });

    // Not Ready
    player.addListener('not_ready', ({ device_id }) => {
        console.log('Device ID has gone offline', device_id);
    });

    // Connect to the player!
    player.connect();
};

//Get access token from url
var url_string = window.location.href; 
var url = new URL(url_string);
var access_token = url.searchParams.get("access_token");
console.log("Url for siden" + window.location.href)
console.log("Access token (taget fra Url)" + access_token)