

function notify(c_index_code) {
    var message = { 'c_index_code': c_index_code };
    if (window.webkit)
        window.webkit.messageHandlers.deviceInfo.postMessage(message);
}

function updateGPS(lo, la) {
    position_overlay.setPosition(ol.proj.transform([parseFloat(lo) - offset.x, parseFloat(la) - offset.y], 'EPSG:4326', 'EPSG:3857'));
}

/**

**/
function loacalByDevice(c_index_code) {
    var feature = vectorSourcedby.getSource().getFeatureById(c_index_code);
    if (!feature) { vectorSourceqt.getSource().getFeatureById(c_index_code); }
    if (feature) {
        var type = feature.get("c_name").substring(feature.get("c_name").length - 3);
        switch (type) {
            case "单兵仪":
                feature.setStyle(markicon.danbingsel);
                break;
            default:
                feature.setStyle(markicon.sel);
                break;
        }
        if (values.select_c_index_code && values.select_c_index_code != c_index_code) {
            var lasfeature = vectorSourcedby.getSource().getFeatureById(values.select_c_index_code);
            if (!lasfeature) { vectorSourceqt.getSource().getFeatureById(values.select_c_index_code); }
            if (lasfeature) { ResteLastFeature(lasfeature); }
        }
        values.last_c_index_code = values.select_c_index_code;
        values.select_c_index_code = c_index_code;
        notify(c_index_code);
        var coordinates = feature.getGeometry().getCoordinates();
        point_overlay.setPosition(coordinates);
        var view = map.getView();
        view.animate({ zoom: view.getZoom() }, { center: coordinates }, function () {
        //    setTimeout(function () { point_overlay.setPosition([0, 0]) }, 30000);

        });
        return;
    }
}

function loacalByLola(lo, la) {
    var coordinates = ol.proj.transform([parseFloat(point.lo) - offset.x, parseFloat(point.la) - offset.y], 'EPSG:4326', 'EPSG:3857')
    point_overlay.setPosition(coordinates);
    var view = map.getView();
    view.animate({ zoom: view.getZoom() }, { center: coordinates }, function () {
        setTimeout(function () { point_overlay.setPosition([0, 0]) }, 30000)
    });
}
