// 自定义分辨率和瓦片坐标系
var resolutions = [];
var maxZoom = 18;

// 计算百度使用的分辨率
for (var i = 0; i <= maxZoom; i++) {
    resolutions[i] = Math.pow(2, maxZoom - i);
}
var tilegrid = new ol.tilegrid.TileGrid({
    origin: [0, 0],
    resolutions: resolutions    // 设置分辨率
});

// 创建百度地图的数据源
var baiduSource = new ol.source.TileImage({
    projection: 'EPSG:3857',
    tileGrid: tilegrid,
    tileUrlFunction: function (tileCoord, pixelRatio, proj) {
        var z = tileCoord[0];
        var x = tileCoord[1];
        var y = tileCoord[2];

        // 百度瓦片服务url将负数使用M前缀来标识
        if (x < 0) {
            x = -x;
        }
        if (y < 0) {
            y = -y;
        }

        return "../baidu/tiles/" + z + "/" + x + "/" + y + ".jpg";
    }
});

var controls = new Array();
/**
var mousePositionControl = new ol.control.MousePosition({
    className: 'custom-mouse-position',
    target: document.getElementById('location'),
    coordinateFormat: ol.coordinate.createStringXY(5),//保留5位小数  
    undefinedHTML: ' ',
    projection: 'EPSG:4326'
});

controls.push(mousePositionControl);
**/

//controls.push(new ol.control.OverviewMap({}));
controls.push(new ol.control.FullScreen());
//缩放至范围  
//var zoomToExtentControl = new ol.control.ZoomToExtent({
//    extent: bounds,
//    className: 'zoom-to-extent',
//    tipLabel: "全图"
//});
//controls.push(zoomToExtentControl);

//比例尺  
var scaleLineControl = new ol.control.ScaleLine({});
controls.push(scaleLineControl);



//缩放控件  
var zoomSliderControl = new ol.control.ZoomSlider({});
controls.push(zoomSliderControl);




// 百度地图层
var baiduMapLayer2 = new ol.layer.Tile({
    source: baiduSource,
    title: '百度',
    type: 'base',
    visible: true,
});

//封装底图函数
function getLayer(layername) {
    return new ol.layer.Tile({
        title: layername,
        tileSize: 256,
        source: new ol.source.XYZ({
            url: "../TMS/{z}/{x}/{y}.jpg"
        })
    });
};

var TiandiTu = getLayer("天地图");


// 创建地图
var map = new ol.Map({
    layers: [ new ol.layer.Group({
        'title': '基础图层',
        layers: [TiandiTu]
      })
    ],
    controls: ol.control.defaults({
        attribution: false
    }).extend(controls),
    view: new ol.View({
        // 设置地图中心
        center: ol.proj.transform([121.41071, 28.37096], 'EPSG:4326', 'EPSG:3857'),
        zoom: 12,
        maxZoom: 17,
        minZoom: 10

    }),
    target: document.getElementById('mapdraw')
});





$(function () {
   // setboxsize();
    setInterval("loadmarks()", 15000);
    loadmarks();
   
});

//判断浏览器是否支持geolocation  
if (navigator.geolocation) {
    // getCurrentPosition支持三个参数  
    // getSuccess是执行成功的回调函数  
    // getError是失败的回调函数  
    // getOptions是一个对象，用于设置getCurrentPosition的参数  
    // 后两个不是必要参数  
    var getOptions = {
        //是否使用高精度设备，如GPS。默认是true  
        enableHighAccuracy: true,
        //超时时间，单位毫秒，默认为0  
        timeout: 5000,
        //使用设置时间内的缓存数据，单位毫秒  
        //默认为0，即始终请求新数据  
        //如设为Infinity，则始终使用缓存数据  
        maximumAge: 0
    };

    //成功回调  
    function getSuccess(position) {
        // getCurrentPosition执行成功后，会把getSuccess传一个position对象  
        // position有两个属性，coords和timeStamp  
        // timeStamp表示地理数据创建的时间？？？？？？  
        // coords是一个对象，包含了地理位置数据  
        console.log(position.timeStamp);

        // 估算的纬度  
        console.log(position.coords.latitude);
        // 估算的经度  
        console.log(position.coords.longitude);
        // 估算的高度 (以米为单位的海拔值)  
        console.log(position.coords.altitude);
        // 所得经度和纬度的估算精度，以米为单位  
        console.log(position.coords.accuracy);
        // 所得高度的估算精度，以米为单位  
        console.log(position.coords.altitudeAccuracy);
        // 宿主设备的当前移动方向，以度为单位，相对于正北方向顺时针方向计算  
        console.log(position.coords.heading);
        // 设备的当前对地速度，以米/秒为单位      
        console.log(position.coords.speed);
        // 除上述结果外，Firefox还提供了另外一个属性address  
        if (position.address) {
            //通过address，可以获得国家、省份、城市  
            console.log(position.address.country);
            console.log(position.address.province);
            console.log(position.address.city);
        }
    }
    //失败回调  
    function getError(error) {
        // 执行失败的回调函数，会接受一个error对象作为参数  
        // error拥有一个code属性和三个常量属性TIMEOUT、PERMISSION_DENIED、POSITION_UNAVAILABLE  
        // 执行失败时，code属性会指向三个常量中的一个，从而指明错误原因  
        switch (error.code) {
            case error.TIMEOUT:
                console.log('超时');
                break;
            case error.PERMISSION_DENIED:
                console.log('用户拒绝提供地理位置');
                break;
            case error.POSITION_UNAVAILABLE:
                console.log('地理位置不可用');
                break;
            default:
                break;
        }
    }

    navigator.geolocation.getCurrentPosition(getSuccess, getError, getOptions);
    // watchPosition方法一样可以设置三个参数  
    // 使用方法和getCurrentPosition方法一致，只是执行效果不同。  
    // getCurrentPosition只执行一次  
    // watchPosition只要设备位置发生变化，就会执行  
    var watcher_id = navigator.geolocation.watchPosition(getSuccess, getError, getOptions);
    //clearwatch用于终止watchPosition方法  
    navigator.geolocation.clearWatch(watcher_id);
}
