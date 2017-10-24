var vectordata = new ol.source.Vector({
    features: [] //add an array of features
});
var offset = { x: 0.006641, y: 0.160775 }; //28.6699850000,121.5158010000---28.50921,121.50916


var vectordataqt = new ol.source.Vector({
    features: [] //add an array of features
});



var vectorSourcedby = new ol.layer.Vector({
    source: vectordata,
    title: '单兵仪',
    visible: true
});

var vectorSourceqt = new ol.layer.Vector({
    source: vectordataqt,
    title: '其他',
    visible: true
});




//定位层
var point_div = document.createElement('div');
point_div.className = "css_animation";
point_overlay = new ol.Overlay({
    element: point_div,
    positioning: 'center-center'
});
map.addOverlay(point_overlay);


//自身位置
var position_div = document.createElement('div');
var e = document.createElement('div');
e.className = "pad_gps_animation";
position_div.appendChild(e);
position_div.className = "pad_gps";
position_overlay = new ol.Overlay({
    element: position_div,
    positioning: 'center-center'
});
map.addOverlay(position_overlay);

map.addLayer(
    new ol.layer.Group({
    title: '非普电力',
    layers: [
       vectorSourcedby,
       vectorSourceqt
    ]
    }));


function loadmarks() {

    var bounds = ol.proj.transformExtent(map.getView().calculateExtent(map.getSize()), 'EPSG:3857', 'EPSG:4326').toString();
    var boundsarr = bounds.split(",");
    var llo= parseFloat(boundsarr[0]) + offset.x;
    var lla = parseFloat(boundsarr[1]) + offset.y;
    var rlo = parseFloat(boundsarr[2]) + offset.x;
    var rla = parseFloat(boundsarr[3]) +offset.y;
    bounds = llo + "," + lla + "," + rlo + "," + rla;
   
  
    var data =
        {
          
            bounds:bounds

        }

    $.ajax({
        type: "POST",
        url: "../Handle/Service/GetaMarksbyBound.ashx",
        data: data,
        dataType: "json",
        success: function (data) {
            if (data.r == "0") {
                if (data.result == "0") {
                    vectorSourcedby.getSource().clear();//单兵
                    vectorSourceqt.getSource().clear();//摄像仪
                    
          
                }
                else {
                    addmarks(data.result);
                    loacalByDevice('001133');
                }
            } else {
                vectorSourcedby.getSource().clear();
                vectorSourceqt.getSource().clear();
              
            }
            

        },
        error: function (msg) {
            console.debug("错误:ajax");

        }
    });

}



function ResteLastFeature(feature){
    var type = feature.get("c_name").substring(feature.get("c_name").length - 3);
    switch (type) {
        case "单兵仪":
           
            if (feature.get("i_status") == "0") { feature.setStyle(markicon.danbingoffline); }
            else {
                feature.setStyle(markicon.danbingonline);
            }

            break;
        default:

            if (feature.get("i_status") == "0") { feature.setStyle(markicon.offline); }
            else {
                feature.setStyle(markicon.online);
            }
            break;
    }
}



map.on('click', function (evt) {
    point_overlay.setPosition([0, 0]);
    var feature = map.forEachFeatureAtPixel(evt.pixel,
        function (feature) {
            return feature;
        });
    if (feature) {
        var coordinates = feature.getGeometry().getCoordinates();     
        point_overlay.setPosition(coordinates);
        setTimeout(function () { point_overlay.setPosition([0, 0]) }, 30000);//三秒后关闭
        switch (feature.get("c_name").substring(feature.get("c_name").length - 3)) {
            case "单兵仪":
                feature.setStyle(markicon.danbingsel);
                break;

            default:
                feature.setStyle(markicon.sel);
                break;
        }
       

        values.last_c_index_code = values.select_c_index_code;

        var flast1 = vectorSourcedby.getSource().getFeatureById(values.last_c_index_code);
        var flast2 = vectorSourceqt.getSource().getFeatureById(values.last_c_index_code);
        if (flast1) {
            ResteLastFeature(flast1);
        }
        if (flast2) {
            ResteLastFeature(flast2);
        }
        values.select_c_index_code = feature.get('c_index_code');
        notify(values.select_c_index_code);
    } 
});

// change mouse cursor when over marker
map.on('pointermove', function (e) {
    if (e.dragging) {
        point_overlay.setPosition([0, 0]);
        return;
    }
    var pixel = map.getEventPixel(e.originalEvent);
    var hit = map.hasFeatureAtPixel(pixel);
    map.getTarget().style.cursor = hit ? 'pointer' : '';
});




function addmarks(points) {



    vectorSourcedby.getSource().clear();
    vectorSourceqt.getSource().clear();

    for (var i = 0; i < points.length; i++) {
        var point = points[i];
        if (point.longitude < 90) { continue;}
        var iconFeature = new ol.Feature({
            geometry: new ol.geom.Point(ol.proj.transform([parseFloat(point.longitude - offset.x), parseFloat(point.latitude - offset.y)], 'EPSG:4326', 'EPSG:3857')),
            c_name: point.c_name,
            c_index_code: point.c_index_code,
            i_status: point.i_status
        });
      
        var type = point.c_name.substring(point.c_name.length - 3);
        
        Seticon(type, point.i_status, point.c_index_code, iconFeature);
        iconFeature.setId(point.c_index_code);
        iconFeature.on()

        switch (point.c_name.substring(point.c_name.length - 3)) {
           

                case "单兵仪":
                    vectorSourcedby.getSource().addFeature(iconFeature);
                    break;
        
                default:
                   vectorSourceqt.getSource().addFeature(iconFeature);
                  break;
            }

        //updateGPS(point.longitude, point.latitude);
      //  point_overlay.setPosition(ol.proj.transform([parseFloat(point.La), parseFloat(point.Lo)], 'EPSG:4326', 'EPSG:3857'));
    }

}

function Seticon(type, i_status, c_index_code, feature) {
    
    switch (type )
    {
        case "单兵仪":
            if (values.select_c_index_code == c_index_code) {
                feature.setStyle(markicon.danbingsel);
                return;
            }
            if (i_status == "0") { feature.setStyle(markicon.danbingoffline); }
             else{
                feature.setStyle(markicon.danbingonline);
            }
           
            break;
        default:
            if (values.select_c_index_code == c_index_code) {
                feature.setStyle(markicon.sel);
                return;
            }
            if (i_status == "0") { feature.setStyle(markicon.offline); }
            else {
                feature.setStyle(markicon.online);
            }
            break;
    }
   
}


var view = map.getView();

view.on('change:resolution', function (e) {
 //   loadmarks();
});
view.on('change:center', function (e) {
//    loadmarks();
});