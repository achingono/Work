/// <reference path="jquery-2.2.4.js" />
/// <reference path="knockout-3.4.0.debug.js" />
(function ($, ko) {

	// add a knockout binding
	ko.bindingHandlers.append = {
	    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
	        /// <signature>
	        /// <summary>
	        /// This will be called when the binding is first applied to an element
	        /// Set up any initial state, event handlers, etc. here
	        /// </summary>
	        /// <param name="element" type="HTMLElement">The DOM element involved in this binding.</param>
	        /// <param name="valueAccessor" type="Function">A JavaScript function that provides the current model property involved in this binding.</param>
	        /// <param name="allBindings" type="PlainObject">A JavaScript object used to access all the model values bound to this DOM element.</param>
	        /// <param name="viewModel" type="PlainObject">This parameter is deprecated in Knockout 3.x. Use bindingContext instead</param>
	        /// <param name="bindingContext" type="PlainObject">An object that holds the binding context available to this element’s bindings. </param>
	        /// </signature>

	        var $element = $(element);
	        // get the value supplied to the binding
	        var value = ko.unwrap(valueAccessor());
	        var attribute = ko.unwrap(value.attribute);
	        // get the current attribute value
	        var original = $element.attr(attribute);
	        // store the initial value of the attribute for use later
	        $element.data("ko-append-original", original);
	    },
	    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
			/// <signature>
			/// <summary>
			/// This will be called when the binding is first applied to an element
			/// Set up any initial state, event handlers, etc. here
			/// </summary>
			/// <param name="element" type="HTMLElement">The DOM element involved in this binding.</param>
			/// <param name="valueAccessor" type="Function">A JavaScript function that provides the current model property involved in this binding.</param>
			/// <param name="allBindings" type="PlainObject">A JavaScript object used to access all the model values bound to this DOM element.</param>
			/// <param name="viewModel" type="PlainObject">This parameter is deprecated in Knockout 3.x. Use bindingContext instead</param>
			/// <param name="bindingContext" type="PlainObject">An object that holds the binding context available to this element’s bindings. </param>
			/// </signature>

			var $element = $(element);
			// get the value supplied to the binding
			var value = ko.unwrap(valueAccessor());
			var attribute = ko.unwrap(value.attribute);
			var separator = ko.unwrap(value.separator);
			var suffix = ko.unwrap(value.value);

			// get the initial value
			var prefix = $element.data("ko-append-original");

			// update the attribute
			$element.attr(attribute, prefix + separator + suffix);
		}
	};

    // add a knockout binding
	ko.bindingHandlers.replace = {
	    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
	        /// <signature>
	        /// <summary>
	        /// This will be called when the binding is first applied to an element
	        /// Set up any initial state, event handlers, etc. here
	        /// </summary>
	        /// <param name="element" type="HTMLElement">The DOM element involved in this binding.</param>
	        /// <param name="valueAccessor" type="Function">A JavaScript function that provides the current model property involved in this binding.</param>
	        /// <param name="allBindings" type="PlainObject">A JavaScript object used to access all the model values bound to this DOM element.</param>
	        /// <param name="viewModel" type="PlainObject">This parameter is deprecated in Knockout 3.x. Use bindingContext instead</param>
	        /// <param name="bindingContext" type="PlainObject">An object that holds the binding context available to this element’s bindings. </param>
	        /// </signature>

	        var $element = $(element);
	        // get the value supplied to the binding
	        var value = ko.unwrap(valueAccessor());
	        var attribute = ko.unwrap(value.attribute);
	        var separator = ko.unwrap(value.separator);
	        var match = ko.unwrap(value.match);
	        var replacement = ko.unwrap(value.value);

	        // get the current attribute value
	        var source = $element.attr(value.attribute);
	        var replaced = source.replace(separator + match, separator + replacement)

	        // update the attribute
	        $element.attr(attribute, replaced);
	    }
	};

}(window["jQuery"], window['ko']));