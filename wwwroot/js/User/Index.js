$(document).ready(function () {
    $('.btn-link.text-danger').click(function (e) {
    e.preventDefault();
    var userId = $(this).data('id');  
    console.log('User ID:', userId);  
    $('#deleteConfirmationModal').modal('show');  
    $('#confirmDeleteBtn').data('id', userId);  
    console.log('Data set on confirmDeleteBtn:', $('#confirmDeleteBtn').data('id'));  
    });

    // $('#deleteConfirmationModal').on('hidden.bs.modal', function () {
    //     $('.modal-backdrop').remove();
    // });
    $('#confirmDeleteBtn').click(function () {
    var userId = $(this).data('id');
    console.log('Sending request to: /User/Delete/' + userId);  
    $.ajax({
        url: '/User/Delete/' + userId,  
        method: 'POST',
        data: { id: userId,
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
         },
        beforeSend: function() {
            console.log("Before sending the request...");
        },
        success: function (response) {
            if (response.success) {
                alert('User deactivated successfully');
                window.location.reload(); 
            } else {
                alert('Something went wrong. Please try again.');
            }
        },
        error: function (xhr, status, error) {
            console.log("Error Status:", status); 
            console.log("Error Thrown:", error);  
            console.log("Response Text:", xhr.responseText); 
            alert('An error occurred. Please try again.');
        }
        });
        $('#deleteConfirmationModal').modal('hide');  
    });
});