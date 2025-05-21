// Dummy Graph for Revenue
var ctxRevenue = document.getElementById('revenueGraph').getContext('2d');
var revenueGraph = new Chart(ctxRevenue, {
    type: 'line',
    data: {
        labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15'],
        datasets: [{
            label: 'Revenue',
            data: [100, 200, 150, 200, 300, 450, 300, 200, 250, 500, 200, 600, 400, 300, 200],
            borderColor: '#007bff',
            fill: false,
            tension: 0.1
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            x: {
                beginAtZero: true
            },
            y: {
                beginAtZero: true
            }
        }
    }
});

// Dummy Graph for Customer Growth
var ctxCustomer = document.getElementById('customerGrowthGraph').getContext('2d');
var customerGrowthGraph = new Chart(ctxCustomer, {
    type: 'line',
    data: {
        labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        datasets: [{
            label: 'Customer Growth',
            data: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60],
            borderColor: '#007bff',
            fill: false,
            tension: 0.1
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            x: {
                beginAtZero: true
            },
            y: {
                beginAtZero: true
            }
        }
    }
});

// JavaScript to handle the dropdown toggle on click
const userIcon = document.querySelector('.user-icon') || document.getElementById('userIcon');
const popupMenu = document.querySelector('.popup-menu');

// Toggle the visibility of the dropdown when the user clicks the profile icon
userIcon.addEventListener('click', function () {
    popupMenu.classList.toggle('active');  // Toggle the 'active' class to show/hide the dropdown
});

userIcon.addEventListener('mouseover', function() {
    popupMenu.style.display = 'block';  // Show the dropdown menu
});

// Add event listener for 'mouseout' to hide the dropdown menu
userIcon.addEventListener('mouseout', function(event) {
    // popupMenu.style.display = 'none';   // Hide the dropdown menu
    if (!popupMenu.contains(event.relatedTarget)) {
        popupMenu.style.display = 'none';  // Hide only if the user is not inside the menu
    }
});

document.addEventListener('click', function(event) {
    if (!userIcon.contains(event.target) && !popupMenu.contains(event.target)) {
        popupMenu.style.display = 'none';  // Hide the dropdown when clicking outside
    }
});