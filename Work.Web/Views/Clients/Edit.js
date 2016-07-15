/// <reference path="../../Scripts/jquery-2.2.4.js" />
/// <reference path="../../Scripts/knockout-3.4.0.debug.js" />
/// <reference path="../../Scripts/knockout.mapping-latest.debug.js" />
(function ($, ko) {

    $(function () {

        var viewModel = {};
        // retrieve the api url from the HTML markup
        var api = $('[data-api]').data('api');

        // call the web api service
        $.get(api, null, function (data, status, request) {
            // retrieve the model from the response
            // and convert it to a viewModel with obeservables
            ko.mapping.fromJS(data, {}, viewModel);

            // bind the data to the content area
            ko.applyBindings(viewModel);
        });

        $(document).on('click', '.btn-primary', function (e) {
            // prevent default action
            e.preventDefault();

            // call the web api service
            $.ajax({
                url: api,
                type: 'PUT',
                data: ko.mapping.toJSON(viewModel),
                // http://stackoverflow.com/a/21322818/99373
                contentType: "application/json; charset=utf-8"
            })
            .done(function (data, status, request) {
                // update the data in the content area
                ko.mapping.fromJS(data, {}, viewModel);
                // display the alert
                $('.alert-success').show();
            });
        })

        // used for diagnostic purposes
        // to make the viewModel accessible in the console
        window.viewModel = viewModel;
    });

}(window["jQuery"], window['ko']));