
$(function () {
    var client = new WindowsAzure.MobileServiceClient("https://yourservice.azure-mobile.net/",
    "your key"),
        officesList = client.getTable('Offices');
        feedbackList = client.getTable('Feedback');
        requestList = client.getTable('Requests');

        var officeNumber = 0;
        var progressNotification1 = "Yay! Your drink is on its way to room ";
        var progressNotification2 = ".";
        var verification1 = "Your drink is still on its way to ";
        var verification2 = ", but don't worry, it'll be there soon.";
        var cancellation1 = "Your request to room ";
        var cancellation2 = " has been cancelled. Stay hydrated!";
        var delivered = "You drink should be delivered. Enjoy!";
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

    //get the avaliable rooms if there are any
        function setRoomNumbers() {

            officesList.read().done(
                function (results) {
                    if (results.length > 0) {
                        for (var i = 0; i < results.length; i++) {
                            $('#office-location-dropdown').append('<option value="' + results[i].number + '">' + results[i].number + '</option>');
                        }
                        $('#office-location-dropdown').attr("id", "office-location");
                    }
                    else {
                        $('#office-location-input').attr("id", "office-location");
                    }

                },
                function (error) {
                    $('#office-location-input').attr("id", "office-location");
                    handleError(error);
                });

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
                //check if request already exists
                requestList.where({ office: officeNumber, complete: false, cancelled: false }).read().done(
                    function (results) {
                        if (results.length != 0) {
                            $('#notification').html(verification1 + officeNumber + verification2);
                        }
                        else {

                            requestList.insert({ office: officeNumber, complete: false, cancelled: false }).done(
                                function (results) {
                                    var id = results.id;
                                    waitForCompletion(id);
                                },
                                function (error) {
                                    handleError(error)
                                });;
                            $('#notification').html(progressNotification1 + officeNumber + progressNotification2);
                        }
                        $('#fetch-fridge').fadeIn();
                        displayProgressButtons();
                    },
                    function (error) {
                        handleError(error)
                    });
            }

            e.preventDefault();
        });

        function waitForCompletion(id) {

            var intervalID = setInterval(function () {
                requestList.where({ id: id, complete: true, cancelled: false }).read().done(
                    function (results) {
                        if (results.length != 0) {
                            $('#notification').html(delivered);
                            clearInterval(intervalID);
                            displayCancelButtons();
                        }
                    });
                requestList.where({ id: id, cancelled: true }).read().done(
                    function (results) {
                        if (results.length != 0) {
                            clearInterval(intervalID);
                        }
                    });
            }, 10000);
        }

        $('#cancel').click(function (e) {
            requestList.where({ office: officeNumber, complete: false, cancelled: false }).read().done(
              function (results) {
                  if (results.length != 0) {
                      requestList.update({ id: results[0].id, office: officeNumber, complete: false, cancelled: true }).done(
                            function (success) { },
                            function (error) {
                                handleError(error)
                            });
                  }
              },
               function (error) {
                   handleError(error)
               });
            $('#notification').html(cancellation1 + officeNumber + cancellation2);
            displayCancelButtons();

            e.preventDefault();
        });

        $('#feedback').click(function (e) {
            $('#feedbackForm').toggle();
            e.preventDefault();
        });

        $('#fridgeLost').click(function (e) {
            $('#notifyMaker').toggle();
            e.preventDefault();
        });

        $('#submit-fb').click(function (e) {

            var text = $('#feedback-text').val();
            if (text) {
                feedbackList.insert({ text: text }).done(
                    function (success) { },
                    function (error) {
                        handleError(error)
                    });
                $('#thank-you').html("Thank you!");

                setTimeout(function () {
                    $('#thank-you').html("");
                    $('#feedbackForm').hide();
                    $('#feedback-text').val("");
                }, 2000);

            }


            e.preventDefault();
        });

        $('#notify').click(function (e) {

            var text = "HURRY! THE ROBOT GOT LOST AND NEEDS YOUR HELP!";
            feedbackList.insert({ text: text }).done(
                    function (success) { },
                    function (error) {
                        handleError(error)
                    });
            $('#thank-you2').html("Thank you!");

            setTimeout(function () {
                $('#thank-you2').html("");
                $('#notifyMaker').hide();
            }, 2000);

            e.preventDefault();
        });

    // On initial load
        refresh();
        setRoomNumbers();

});