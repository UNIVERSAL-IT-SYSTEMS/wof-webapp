var SendGrid = require('sendgrid').SendGrid;

function insert(item, user, request) {    
    request.execute({
        success: function() {
            // After the record has been inserted, send the response immediately to the client
            request.respond();
            // Send the email in the background
            sendEmail(item);
        }
    });

    function sendEmail(item) {
        var sendgrid = new SendGrid('[your account]@azure.com', '[your key]');       

        sendgrid.send({
            to:  ['youremail@host.com'],
            from: 'youremail@host.com',
            subject: 'You have new feedback!',
            text: "Here is the new feedback: " + item.text 
        }, function(success, message) {
            // If the email failed to send, log it as an error so we can investigate
            if (!success) {
                console.error(message);
            }
        });
    }
}