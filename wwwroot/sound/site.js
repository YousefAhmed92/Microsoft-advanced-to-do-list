//window.playSuccessSound = function () {
//    let audio = new Audio("sound/successed-295058.mp3");
//    audio.play();
//};
window.playCompletionSound = function () {
    var audio = document.getElementById("completionSound");
    if (audio) {
        audio.play();
    }
};
