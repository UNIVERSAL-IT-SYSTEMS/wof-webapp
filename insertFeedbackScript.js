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
        var sendgrid = new SendGrid('azure_b9e9d55bfa219948461f97a9d051419a@azure.com', 'Hm1B4bjQyw68iWp');       

        sendgrid.send({
            to:  ['le.explorer.intern@outlook.com'],
            from: 'mini-fridge@outlook.com',
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