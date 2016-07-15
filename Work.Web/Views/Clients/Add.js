/// <reference path="../../Scripts/jquery-2.2.4.js" />
/// <reference path="../../Scripts/knockout-3.4.0.debug.js" />
/// <reference path="../../Scripts/knockout.mapping-latest.debug.js" />
(function ($, ko) {

    $(function () {

        var viewModel = {};
        // retrieve the api url from the HTML markup
        var api = $('[data-api]').data('api');
        // retrieve the model from the HTML markup
        // and convert it to a viewModel with obeservables
        ko.mapping.fromJS($('[data-model]').data('model'), {}, viewModel);

        // bind the data to the content area
        ko.applyBindings(viewModel);

        $(document).on('click', '.btn-primary', function (e) {
            // prevent default action
            e.preventDefault();

            // get a reference to the button that was clicked
            var button = $(e.target);
            // disable the button
            button.addClass("disabled");

            // call the web api service
            $.ajax({
                url: api,
                type: 'POST',
                data: ko.mapping.toJSON(viewModel),
                // http://stackoverflow.com/a/21322818/99373
                contentType: "application/json; charset=utf-8"
            })
            .done(function (data, status, request) {
                // update the data in the content area
                ko.mapping.fromJS(data, {}, viewModel);
                // display the success message
                $('.alert-success').removeClass("hide");
                // hide the error message
                $('.alert-danger:not(.hide)').addClass("hide");
            }).fail(function (request, status, message){
                // display the alert message
                $('.alert-danger').removeClass("hide");
                // re-enable the button
                button.removeClass("disabled");
            });
        })

        window.viewModel = viewModel;
    });

}(window["jQuery"], window['ko']));