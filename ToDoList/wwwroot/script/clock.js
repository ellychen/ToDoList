
let TargetTime = null;

function startTime() {
    if (TargetTime == null) return;

    var times = ConvertTime(Math.floor((new Date()  - TargetTime) / 1000));
    let mm = checkTime(times[1]);
    $(".clock").html(times[0] + ":" + mm);   
    var timeoutId = setTimeout(startTime, 30000);
}

function ConvertTime(sec) {    
    let val = 60;
    var iSec = sec % val;
    sec = Math.floor(sec / val);
    var iMinute = sec % val;
    var iHour = Math.floor(sec / val);

    return [iHour, iMinute];
}

function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}
