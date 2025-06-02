function exportCustomerData() {
    // Get filter values
    var searchTerm = document.querySelector('input[name="searchTerm"]')?.value || '';

    // Build the export URL
    var url = `/SuperAdmin/ExportCustomersToExcel?searchTerm=${encodeURIComponent(searchTerm)}`;

    // Redirect to trigger download
    window.location.href = url;
}
