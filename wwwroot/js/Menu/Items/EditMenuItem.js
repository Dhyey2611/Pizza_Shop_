document.addEventListener("DOMContentLoaded", function () {
    const modal = new bootstrap.Modal(document.getElementById("EditMenuItemModal"));
    document.body.addEventListener("click", function (e) {
        if (e.target.closest(".edit-menu-item-btn")) {
            const button = e.target.closest(".edit-menu-item-btn");
            console.log("Edit button clicked:", button);
            const itemId = parseInt(button.getAttribute("data-id"));
            console.log("itemId is:", itemId);

            fetch(`/SuperAdmin/GetMenuItem/${itemId}`)
                .then(res => res.json())
                .then(data => {
                    console.log("Fetched Data from Server:", data); 
                    modal.show();
                    setTimeout(() => {
                    document.getElementById("MenuItemId").value = data.menuItemId;
                    document.getElementById("floatingName").value = data.name;
                    document.getElementById("floatingCategory").value = data.categoryId;
                    document.getElementById("floatingItemType").value = data.itemType;
                    document.getElementById("floatingRate").value = data.rate;
                    document.getElementById("floatingQuantity").value = data.quantity;
                    document.getElementById("floatingunit").value = data.unit;
                    document.getElementById("floatingtax").value = data.taxPercentage;
                    document.getElementById("floatingcode").value = data.shortCode;
                    document.getElementById("floatinginfo").value = data.description;
                    document.querySelector('input[name="IsAvailable"]').checked = data.isAvailable;
                    document.querySelector('input[name="DefaultTax"]').checked = data.defaultTax;
                    },200);
                });
            }
    });
});

