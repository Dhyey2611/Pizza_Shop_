// function exportCustomerData() {
//     var searchTerm = document.querySelector('input[name="searchTerm"]')?.value || '';
//     var fromDate = document.getElementById("fromDate")?.value || '';
//     var toDate = document.getElementById("toDate")?.value || '';
//     console.log("Export Triggered");
//     console.log("Search Term:", searchTerm);
//     console.log("From Date:", fromDate);
//     console.log("To Date:", toDate);
//     var url = `/SuperAdmin/ExportCustomersToExcel?searchTerm=${encodeURIComponent(searchTerm)}&fromDate=${encodeURIComponent(fromDate)}&toDate=${encodeURIComponent(toDate)}`;
//     window.location.href = url;
// }
function exportCustomerData() {
    const urlParams = new URLSearchParams(window.location.search);

    var searchTerm = urlParams.get('searchTerm') || '';
    var fromDate = urlParams.get('fromDate') || '';
    var toDate = urlParams.get('toDate') || '';

    console.log("Export Triggered");
    console.log("Search Term:", searchTerm);
    console.log("From Date:", fromDate);
    console.log("To Date:", toDate);

    var url = `/SuperAdmin/ExportCustomersToExcel?searchTerm=${encodeURIComponent(searchTerm)}&fromDate=${encodeURIComponent(fromDate)}&toDate=${encodeURIComponent(toDate)}`;
    window.location.href = url;
}
