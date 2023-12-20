window.app = window.app || {};
$.extend(app, { map: undefined, taiwancenter: [23.7, 121] });
$.AppConfigOptions.require.all(function () {

    app.map = L.map('map', { zoomControl: false, trackResize: true });
     
    //底圖
    var baseoptions = { map: app.map };
    $.extend(baseoptions, $.MapBaseLayerDefaultSettings);
    //baseoptions.tiles.other1 = {
    //    id: "other2",
    //    name: "其他",
    //    groupTiles: [
    //        $.MapBaseLayerDefaultSettings.ext.Google.silver,
    //        $.MapBaseLayerDefaultSettings.ext.Google.gray,
    //        $.MapBaseLayerDefaultSettings.ext.Google.retro,
    //        $.MapBaseLayerDefaultSettings.ext.Google.dark,
    //        //$.MapBaseLayerDefaultSettings.ext.Google.night_mode,
    //        $.MapBaseLayerDefaultSettings.ext.Google.hide_featires,
    //        $.MapBaseLayerDefaultSettings.ext.TGOS.通用版電子地圖
    //    ]
    //}
    $('#basemapDiv').MapBaseLayer(baseoptions);

    app.map.setView(app.taiwancenter, 8);
    //座標資訊
    $('#coordinateInfoDiv').CoordinateInfo({ map: app.map, display: $.CoordinateInfo.display.WGS84_TWD97, content_padding: 1, initEvent: $.menuctrl.eventKeys.popu_init_before });

    $("#_twfullext").click(function () {
        app.map.setView(app.taiwancenter, 8);
    });

    $("#_zoomin").click(function () {
        if (app.map.getMaxZoom) {
            if (app.map.getZoom() != app.map.getMaxZoom())
                app.map.setZoom(app.map.getZoom() + 1);
        }
        else
            app.map.setZoom(app.map.getZoom() + 1);

    });
    $("#_zoomout").click(function () {
        if (app.map.getMinZoom) {
            if (app.map.getZoom() != app.map.getMinZoom())
                app.map.setZoom(app.map.getZoom() - 1);
        } else
            app.map.setZoom(app.map.getZoom() - 1);

    });

    $.initGisMenu('mainmenu'); //初始化menu
    $_CarFuelCtrl = InitGasCtrl({ name: '汽機車加油站', url: app.siteRoot + 'api/CarFuel/Base', legendIcons: [{ 'name': '圖例', 'url': app.siteRoot + 'Images/pin/汽機車加油站.png', classes: 'blue_status' }], });
    $_CarGasCtrl = InitGasCtrl({ name: '汽車加氣站', url: app.siteRoot + 'api/CarGas/Base', legendIcons: [{ 'name': '圖例', 'url': app.siteRoot + 'Images/pin/汽車加氣站.png', classes: 'blue_status' }], });
    $_FishGasCtrl = InitGasCtrl({ name: '漁船加油站', url: app.siteRoot + 'api/FishGas/Base', legendIcons: [{ 'name': '圖例', 'url': app.siteRoot + 'Images/pin/漁船加油站.png', classes: 'blue_status' }], });
    $_SelfFuelCtrl = InitGasCtrl({ name: '自用加儲油站', url: app.siteRoot + 'api/SelfFuel/Base', legendIcons: [{ 'name': '圖例', 'url': app.siteRoot + 'Images/pin/自用加儲油站.png', classes: 'blue_status' }], });
    $_SelfGasCtrl = InitGasCtrl({ name: '自用加儲氣站', url: app.siteRoot + 'api/SelfGas/Base', legendIcons: [{ 'name': '圖例', 'url': app.siteRoot + 'Images/pin/自用加儲氣站.png', classes: 'blue_status' }], });
    InitPosition();
})
var $_CarFuelCtrl, $_CarGasCtrl, $_FishGasCtrl, $_SelfFuelCtrl, $_SelfGasCtrl;

var setAllFilter = function (center, radiusmeters) {
    setFilter($_CarFuelCtrl, center, radiusmeters);
    setFilter($_CarGasCtrl, center, radiusmeters);
    setFilter($_FishGasCtrl, center, radiusmeters);
    setFilter($_SelfFuelCtrl, center, radiusmeters);
    setFilter($_SelfGasCtrl, center, radiusmeters);
}
var setFilter = function ($_p,  center, radiusmeters) {
    $_p.PinCtrl('setFilter', function (d) {
        if (center == undefined)
            return true;
        var pd = helper.gis.pointDistance([center.lng, center.lat], [d.X, d.Y]);
        return pd <= radiusmeters;
    });
}
var InitGasCtrl = function (_options) {
    var _op = $.extend({map:app.map}, gasOption, _options);
    var $_mainctrl = $('#main-ctrl');
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_mainctrl).find('.col-md-12').PinCtrl(_op);
    return $_p;
}

var gasOption = {
    stTitle: function (d) { return d.Gas_Name },
    pinInfoLabelMinWidth:'66px',
    //listTheme:'info',
    infoFields: [
        { field: 'Gas_Name', title: '名稱' },
        { field: 'Business_theme', title: '營業主體' },
        { field: 'Address', title: '地址' },
        { field: 'UsageState', title: '營運狀況' }
    ],
    transformData: function (_base, _info) {
        var _that = this;
        var _result = [];
        $.each(_base, function () {
            var b = this;
            if (this.TWD97_X) {
                var coor = helper.gis.TWD97ToWGS84(this.TWD97_X, this.TWD97_Y);
                b.X = coor.lon;
                b.Y = coor.lat;
                _result.push(b);
            }
        });
        return _result;
    },
    loadBase: function (callback) {
        helper.data.get(this.settings.url, function (ds) {
            callback(ds);
        });
    },
    loadInfo: function (dt, callback) { callback([])}
}

var InitPosition = function () {
    var $_ctrl = $('#position-ctrl');
    var currentCoor, currentCircle;
    var $_addressContainer = $('<div class="ctrl-container"></div>').appendTo($_ctrl)
        .addressGeocode({ map: app.map }).on($.addressGeocode.eventKeys.ui_init_completed, function () {
            //that.$_addressContainer.find('.btn').addClass('btn btn-outline-info');
        }).on($.addressGeocode.eventKeys.select_change, function (e, coor) {
            currentCoor = coor;
            console.log(coor);
            //that.clear(true);
            ////enableClearGeoBtn();
            //$cancel.removeClass("disabled");
        });
    var $_r = $('<div class="input-group  mt-3"><input type="number" class="form-control" placeholder="輸入範圍"><span class="input-group-addon btn circle-radius-desc">公里</span></div>').appendTo($_ctrl).find('input');

    var $_temp = $('<div class="container mt-3"><div class="row justify-content-around col-12"></div>').appendTo($_ctrl).find('>div');
     $('<span class="glyphicon glyphicon-map-marker col-5 btn btn-success" title="定位">定位</span>').appendTo($_temp).on('click', function () {
        var r = $_r.val();
        if (!r || !currentCoor)
            alert('先輸入地址及範圍');
        else {
            if (currentCircle)
                currentCircle.remove();
            currentCircle = L.circle(currentCoor, { radius: r * 1000 }).addTo(app.map);
            app.map.fitBounds(currentCircle.getBounds());
            setAllFilter(currentCoor, r * 1000);
        }
    });
    $('<span class="glyphicon glyphicon-erase col-5 btn btn-warning" title="清除">清除</span>').appendTo($_temp).on('click', function () {
        $_addressContainer.addressGeocode('clear');
        currentCoor = undefined;
        if (currentCircle)
            currentCircle.remove();
        setAllFilter(undefined, undefined);
    });
}