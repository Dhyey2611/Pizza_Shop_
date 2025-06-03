function formatDateTime(isoString) {
    const date = new Date(isoString);
    const formattedDate = date.toLocaleDateString('en-GB'); // dd-mm-yyyy
    const formattedTime = date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }); // HH:MM
    return `${formattedDate} ${formattedTime}`;
}
function loadCustomerHistory(customerId) {
    fetch(`/SuperAdmin/GetCustomerHistory?customerId=${customerId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not OK');
            }
            return response.json();
        })
        .then(data => {
            // Fill hidden fields
            document.getElementById('hiddenName').value = data.name;
            document.getElementById('hiddenMobile').value = data.mobileNumber;
            document.getElementById('hiddenComingSince').value = data.comingSince;
            document.getElementById('hiddenVisits').value = data.visits;
            document.getElementById('hiddenAverageBill').value = data.averageBill;
            document.getElementById('hiddenMaxOrder').value = data.maxOrder;

            // Update visible spans from hidden values
            document.getElementById('viewName').textContent = data.name;
            document.getElementById('viewMobile').textContent = data.mobileNumber;
            document.getElementById('viewComingSince').textContent = formatDateTime(data.comingSince);
            document.getElementById('viewVisits').textContent = data.visits;
            document.getElementById('viewAverageBill').textContent = data.averageBill;
            document.getElementById('viewMaxOrder').textContent = data.maxOrder;

            // Fill history table
            const tbody = document.getElementById('customerHistoryTableBody');
            tbody.innerHTML = '';

            data.orderHistory.forEach(order => {
                const row = `
                    <tr>
                        <td>${order.orderDate.split('T')[0]}</td>
                        <td>${order.orderType}</td>
                        <td>${order.paymentStatus}</td>
                        <td>${order.itemsCount}</td>
                        <td>${order.amount}</td>
                    </tr>`;
                tbody.innerHTML += row;
            });

            // Show modal
            const modal = new bootstrap.Modal(document.getElementById('customerHistoryModal'));
            modal.show();
        })
        .catch(error => {
            console.error('Fetch error:', error);
            alert('Error loading customer history.');
        });
}
