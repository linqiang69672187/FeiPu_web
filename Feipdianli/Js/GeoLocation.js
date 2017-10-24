if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(locationSuccess, locationError, {
        // 指示浏览器获取高精度的位置，默认为false
        enableHighAccuracy: true,
        // 指定获取地理位置的超时时间，默认不限时，单位为毫秒
        timeout: 5000,
        // 最长有效期，在重复获取地理位置时，此参数指定多久再次获取位置。
        maximumAge: 3000
    });
} else {
    alert("Your browser does not support Geolocation!");
}

function locationError(error){
    switch(error.code) {
        case error.TIMEOUT:
            showError("A timeout occured! Please try again!");
            break;
        case error.POSITION_UNAVAILABLE:
            showError('We can\'t detect your location. Sorry!');
            break;
        case error.PERMISSION_DENIED:
            showError('Please allow geolocation access for this to work.');
            break;
        case error.UNKNOWN_ERROR:
            showError('An unknown error occured!');
            break;
    }
}

function locationSuccess(position) {
    var coords = position.coords;
}