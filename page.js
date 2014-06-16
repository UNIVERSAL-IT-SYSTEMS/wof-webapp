$(function () {
    var client = new WindowsAzure.MobileServiceClient('https://stay-hydrated.azure-mobile.net/', 'pDhaZaEqZoEysZWrcjKVAYRKGjTMYA21'),
        officesList = client.getTable('offices'),
        feedbackList = client.getTable('Feedback');

    var officeNumber = 0;
    var progressNotification1 = "Yay! Your drink is on its way to room ";
    var progressNotification2 = ".";
    var verification1 = "Your drink is still on its way to ";
    var verification2 = ", but don't worry, it'll be there soon.";
    var cancellation1 = "Your request to room ";
    var cancellation2 = " has been cancelled. Stay hydrated!";
    var error = "Please input the valid office number (do not include your building name)";
    var waitNotification = "fetching the fridge...";

    function displayProgressButtons() {
        $('#go').hide();
        $('#cancel').show();
    }

    function displayCancelButtons() {
        $('#go').show();
        $('#cancel').hide();
    }

    function handleError(error) {
        var text = error + (error.request ? ' - ' + error.request.status : '');
        $('#errorlog').append($('<li>').text(text));
    }

    function refresh() {
        $('#notification').html("");
        displayCancelButtons();
    }


    // Handle insert
    $('#go').click(function (e) {
        officeNumber = $('#office-location').val();
        $('#office-location').val("");
        if (!officeNumber || isNaN(officeNumber - 0) || officeNumber <= 0) {
                    officeNumber = 0;
                    $('#errorlog').empty();
                    $('#errorlog').append($('<li>').text(error));
        } 
        else {
            $('#errorlog').empty();
            $('#fetch-fridge').fadeOut();
            $('#notification').html(waitNotification);
            officesList.where({ office: officeNumber, complete: false, cancelled: false }).read().done(
            function (results) {
                if (results.length != 0) {
                    $('#notification').html(verification1 + officeNumber + verification2);
                }
                else {
                    
                    officesList.insert({ office: officeNumber, complete: false, cancelled: false }).done(
                        function () { },
                        function (error) {
                            handleError(error)
                        }); ;
                    $('#notification').html(progressNotification1 + officeNumber + progressNotification2);                   
                }
                $('#fetch-fridge').fadeIn();
                displayProgressButtons();
            });


        }

        e.preventDefault();
    });

    $('#cancel').click(function (e) {
        officesList.where({ office: officeNumber, complete: false, cancelled: false }).read().done(
          function (results) {
              if (results.length != 0) {
                  officesList.update({ id: results[0].id, office: officeNumber, complete: false, cancelled: true }).done(
                        function () { },
                        function (error) {
                            handleError(error)
                        });
              }
          });
        $('#notification').html(cancellation1 + officeNumber + cancellation2);
        displayCancelButtons();

        e.preventDefault();
    });

    $('#feedback').click(function (e) {
        $('#feedbackForm').show();
        e.preventDefault();
    });

    $('#submit-fb').click(function (e) {

        var text = $('#feedback-text').val();
        if (text) {
            feedbackList.insert({ text: text });
            $('#thank-you').html("Thank you!");

            setTimeout(function () {
                $('#thank-you').html("");
                $('#feedbackForm').hide();
                $('#feedback-text').val("");
            }, 2000);

        }


        e.preventDefault();
    });


    // On initial load
    refresh();
});