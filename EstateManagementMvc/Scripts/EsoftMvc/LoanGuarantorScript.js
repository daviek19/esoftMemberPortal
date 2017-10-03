/*
  This script is used when loading _LoanGuarantors.chtml 
 */

$("#RegisterLoanModal").on('shown.bs.modal', function () {
    

    //Hide the delete btn
    $("#delBtn").hide();


    selectedProductCode = $("#SingleLoanRecord_ProductCode option:selected").val();
    

    if (selectedProductCode == "selectedProductCode" || selectedProductCode.length <= 0) {
        toastr.error("First Select A Product Code", "Add Guarantor");
    } else {
        clearRegisterLoanModalContent();
    }

    $('.loader2').hide();
});

$('body').on('blur', "#GuarantorNo", function (e) {
    var guarantorNo = $('#GuarantorNo').val();

    if (guarantorNo.length > 0) {
        $('.loader2').show();

        var data = {
            guarantorNo: guarantorNo,
            loanCode: selectedProductCode
        };

        $.ajax({
            type: "POST",
            url: GetGuarantorDetailsUrl,
            data: data,
            dataType: "json",
            success: function (data) {
                $('.loader2').hide();
                $("#delBtn").hide();
                
                if (data != '') {
                    $('#GuarantorNo').val(data.GuarantorNo);
                    $('#GuarantorIdNo').val(data.GuarantorIdNo);
                    $('#CustomerName').val(data.GuarantorName);
                    $('#GuarantorBalance').val(numberWithCommas(data.GuarantorShareBalance));
                    $('#LoanType').val(selectedProductCode);
                    $("#delBtn").show();
                }
            },

            error: function (response) {
                
                $('#loading').hide();//Dismiss loading
            }
        });
    } else {
        toastr.error("Empty Guarantor", "Add Guarantor");
    }
});

$("body").on("click", "#addGuarantors", function () {
    
    var origId = $("#GuarantorIdNo").val().trim();
    var capturedId = $("#confirmIdNo").val().trim();

    if (!(origId == capturedId)) {
        alert("Guarantor ID No entered does not Match");
        $("#confirmIdNo").focus();
        return false;
    }

    var table = $('.guarantors_tb').DataTable();

    var data = $("#LoanRegistartion").serialize();
    
    $.ajax({
        type: "POST",
        url: PopulateGuarantorsUrl,
        data: data,
        dataType: "json",
        success: function (response) {            
            if (response.status === "00") {
                $('.guarantors_tb').dataTable().fnAddData([
                       response.newloanGuarantor.GuarantorNo,
                       response.newloanGuarantor.CustomerName,
                       response.newloanGuarantor.GuarantorIdNo,
                       numberWithCommas(response.newloanGuarantor.GuaranteedAmount),
                       numberWithCommas(response.newloanGuarantor.GuarantorBalance),                      
                ]);

                $("#RegisterLoanModal").modal("hide");
            }
            else if (response.status === "11") {
                //ajaxShowModelErrors("guarantorFrm", response);
                var errorMessage = "";

                for (var j = 0; j < response.FieldsWithErrors.length; j++) {
                    errorMessage += response.FieldsWithErrors[j].Errors[0]+" <br>";                 
                }

                toastr.error(errorMessage, "Adding Guarantor");
            }
        }
    });
});

function clearRegisterLoanModalContent() {
    
    $('#LoanType').val(selectedProductCode);
    $('#CustomerNo').val($("#SingleLoanRecord_CustomerNo").val());
    $('#GuarantorIdNo').val();
    $('#CustomerName').val();
    $('#GuarantorBalance').val();
    $('.loader2').hide();
}