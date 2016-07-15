/// <reference path="../../Scripts/jquery-2.2.4.js" />
/// <reference path="../../Scripts/knockout-3.4.0.debug.js" />
/// <reference path="../../Scripts/knockout.mapping-latest.debug.js" />
(function ($, ko) {

    $(function () {

        // retrieve the api url from the HTML markup
        var api = $('body').data('api');

        // call the web api service
        $.get(api, null, function (data, status, request) {
            // bind the data to the content area
            ko.applyBindings(data);
        });

    });

}(window["jQuery"], window['ko']));