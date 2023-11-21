$(".toggle-password").click(function () {


   $(this).toggleClass("fa-eye fa-eye-slash");

   var input = $($(this).attr("toggle"));

   if (input.attr("type") == "password") {

      input.attr("type", "text");

   } else {

      input.attr("type", "password");

   }

});

$(document).ready(function () {
    new DataTable('#settings', { responsive: true });

    $('.example').dataTable({
        destroy: true,
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }]
    });
});