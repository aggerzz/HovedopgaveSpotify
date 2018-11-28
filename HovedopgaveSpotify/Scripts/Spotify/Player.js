
function Play(music, id) {
    $.ajax(
        {
            type: "POST", //HTTP POST Method
            url: '/Player/Play',
            data: { //Passing data
                access_token: access_token,

            }
        });
}
function Stop(music, id) {
    $.ajax(
        {
            type: "POST", //HTTP POST Method
            url: '/Player/Pause',
            data: { //Passing data
                access_token: access_token,

            }
        });
}
function VolumeUp() {
    $.ajax(
        {
            type: "POST", //HTTP POST Method
            url: '/Player/VolumeUp',
            data: { //Passing data
                access_token: access_token,

            }
        });
}
function VolumeDown() {
    $.ajax(
        {
            type: "POST", //HTTP POST Method
            url: '/Player/VolumeControl',
            data: { //Passing data
                access_token: access_token,

            }
        });
}
function Next() {

    $.ajax(
        {
            type: "POST", //HTTP POST Method
            url: '/Player/SkipToNext',
            data: { //Passing data
                access_token: access_token,

            }
        });
}
function Previous() {
    $.ajax(
        {
            type: "POST", //HTTP POST Method
            url: '/Player/SkipToPrev',
            data: { //Passing data
                access_token: access_token,

            }
        });
}
function Forward1(music, id) {
    var audio = $("#" + id);
    audio.prop("currentTime", audio.prop("currentTime") + 1);
}
function Backward1(music, id) {
    var audio = $("#" + id);
    audio.prop("currentTime", audio.prop("currentTime") - 1);
}