var markicon = {
   danbingonline : new ol.style.Style({
        image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
            anchor: [0.5, 50],
            anchorXUnits: 'fraction',
            anchorYUnits: 'pixels',
            src: 'IMG/type/phone_online@2x.png'
        }))
    }),
   danbingoffline: new ol.style.Style({
       image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
           anchor: [0.5, 50],
           anchorXUnits: 'fraction',
           anchorYUnits: 'pixels',
           src: 'IMG/type/phone_offline@2x.png'
       }))
   }),
   danbingsel: new ol.style.Style({
       image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
           anchor: [0.5, 50],
           anchorXUnits: 'fraction',
           anchorYUnits: 'pixels',
           src: 'IMG/type/phone_sel@2x.png'
       }))
   }),
  online: new ol.style.Style({
       image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
           anchor: [0.5, 50],
           anchorXUnits: 'fraction',
           anchorYUnits: 'pixels',
           src: 'IMG/type/home_online@2x.png'
       }))
   }),
   offline: new ol.style.Style({
       image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
           anchor: [0.5, 50],
           anchorXUnits: 'fraction',
           anchorYUnits: 'pixels',
           src: 'IMG/type/home_offline@2x.png'
       }))
   }),
   sel: new ol.style.Style({
       image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
           anchor: [0.5, 50],
           anchorXUnits: 'fraction',
           anchorYUnits: 'pixels',
           src: 'IMG/type/home_sel@2x.png'
       }))
   })

}

var values = {
    select_c_index_code: 0,
    last_c_index_code:0

}
