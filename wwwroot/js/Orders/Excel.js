function exportFilteredData() {
        // Get filter values
        var status = document.querySelector('select[name="status"]')?.value || '';
        var searchTerm = document.querySelector('input[name="searchTerm"]')?.value || '';
        var fromDate = document.getElementById("fromDate")?.value || '';
        var toDate = document.getElementById("toDate")?.value || '';

        // Build the export URL
        var url = `/SuperAdmin/ExportToExcel?status=${encodeURIComponent(status)}&searchTerm=${encodeURIComponent(searchTerm)}&fromDate=${encodeURIComponent(fromDate)}&toDate=${encodeURIComponent(toDate)}`;

        // Redirect to trigger download
        window.location.href = url;
}