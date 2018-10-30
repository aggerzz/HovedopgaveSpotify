﻿window.onSpotifyWebPlaybackSDKReady = () => {
    const token = 'BQDM2zbx4BHgdupF95gNvCml9dlEYhwHhQpfuqAHIxpHRw5uRnfsmJL8q3KrSJyblHZaAqsSnqVd_g23R5Mzit-jBWiWsT3QX0ZntdW7_sEUjQde7iZ8cbT-ZTpeHopHD9CdqNxmWp5PPIkrXKko72DD6MIYrwtOyf0vnm8';
    const player = new Spotify.Player({
        name: 'Web Playback SDK Quick Start Player',
        getOAuthToken: cb => { cb(token); }
    });

    // Error handling
    player.addListener('initialization_error', ({ message }) => { console.error(message); });
    player.addListener('authentication_error', ({ message }) => { console.error(message); });
    player.addListener('account_error', ({ message }) => { console.error(message); });
    player.addListener('playback_error', ({ message }) => { console.error(message); });

    // Playback status updates
    player.addListener('player_state_changed', state => { console.log(state); });

    // Ready
    player.addListener('ready', ({ device_id }) => {
        console.log('Ready with Device ID', device_id);
    });

    // Not Ready
    player.addListener('not_ready', ({ device_id }) => {
        console.log('Device ID has gone offline', device_id);
    });

    // Connect to the player!
    player.connect();
};