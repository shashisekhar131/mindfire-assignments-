jQuery.noConflict();

function loadCountries() {
    $.ajax({
        url: '/UserRegistration/GetCountries', // Replace with your actual server endpoint
        type: 'POST',  // Use POST for WebMethods
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            var countries = data;

            var Countrydropdowns = ['PresentCountry', 'PermanentCountry'];
            var Statedropdowns = ['PresentState', 'PermanentState'];

            for (var i = 0; i < Countrydropdowns.length; i++) {
                var CountrydropdownId = Countrydropdowns[i];

                // Get the dropdown element
                var Countrydropdown = $('#' + CountrydropdownId);

                // Clear existing options
                Countrydropdown.empty();

                // Add options from the received data
                for (var j = 0; j < countries.length; j++) {
                    Countrydropdown.append($('<option>', { value: countries[j].CountryID, text: countries[j].CountryName }));
                }

                LoadStatesForCountry(countries[0].CountryID, Statedropdowns[i]);

            }

        },
        error: function (xhr, status, error) {
            console.log('Error loading countries: ', error);
            console.log(xhr.responseText);
        }

    });
}

function LoadStatesForCountry(CountryID, StatedropdownId) {
    console.log(CountryID);
    $.ajax({
        url: '/UserRegistration/GetStatesForCountry',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ SelectedCountryID: CountryID }), // Send the countryName parameter
        dataType: 'json',
        success: function (data) {
            console.log(data);
            var states = data;
            console.log(states[0]);
            var Statedropdown = $('#' + StatedropdownId);

            Statedropdown.empty();

            $.each(states, function (index, state) {
                Statedropdown.append($('<option>', { value: state.StateID, text: state.StateName }));
            });



        },
        error: function (xhr, status, error) {
            console.log('Error loading states: ', error);
            console.log(xhr.responseText);
        }
    });
}

function CountrySelected(CountrydropdownId, StatedropdownId) {
    var selectedCountry = $('#' + CountrydropdownId).val();
    LoadStatesForCountry(selectedCountry, StatedropdownId);
}

jQuery(document).ready(function ($) {

    loadCountries();
    // user selected country
    $('#PresentCountry').on('change', function () {
        CountrySelected('PresentCountry', 'PresentState');
    });
    $('#PermanentCountry').on('change', function () {
        CountrySelected('PermanentCountry', 'PermanentState');
    });


});