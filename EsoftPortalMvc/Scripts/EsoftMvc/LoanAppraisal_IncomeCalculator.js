


function RecalculateAppraisalScreen() {

    var additions = parseFloat($("#SingleLoanRecord_TotalAdditions").val().replace(/,/g, ''))||0; // remove commas
    var deductions = parseFloat($("#SingleLoanRecord_TotalSubstractions").val().replace(/,/g, '')) || 0;
    var totPay = parseFloat($("#SingleLoanRecord_NetPay").val().replace(/,/g, '')) || 0;
    var grossPay = totPay + additions;
    var netPay = grossPay - deductions;
    

    var retention = parseFloat(totPay) / 3;
    var appraisalMethod = parseInt($('input[name="appraisalMode"]:checked').val()) || 0;   
    switch (appraisalMethod) {
        case 0:
            document.getElementById('retentionSection').style.display = 'none';
            retention = totPay / 3;
            break;
        case 1:
            document.getElementById('retentionSection').style.display = 'none';
            retention = grossPay / 3;            
            break;
        case 2:
            document.getElementById('retentionSection').style.display = 'block';
            retention = parseFloat($("#SingleLoanRecord_FixedAmount").val().replace(/,/g, '')) || 0;
            break;
        default:
    }
    retention = Math.round(retention, 2);

    

    $("#SingleLoanRecord_GrossPay").val(numberWithCommas(grossPay));
    $("#MonthlyAbility").val(numberWithCommas(netPay));
    $("#Retention").val(numberWithCommas(retention));    
    $("#FullPay").val(numberWithCommas($("#SingleLoanRecord_NetPay").val()));
    $("#AvailableAmount").val(numberWithCommas(netPay - retention));   
}

