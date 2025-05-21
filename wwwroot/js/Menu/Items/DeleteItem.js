document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll("[data-bs-target='#deleteItemModal']").forEach(btn => {
        btn.addEventListener("click", function () {
            const itemId = parseInt(this.getAttribute("data-id"));
            console.log("Single item delete clicked, itemId:", itemId);
            document.getElementById("deleteItemId").value = itemId;
        });
    });

    const topDeleteButton = document.querySelector(".btn-outline-secondary[data-bs-target='#deleteItemModal']");

    if (topDeleteButton) {
        topDeleteButton.addEventListener("click", function (e) {
            e.preventDefault();
            
            const selectedCheckboxes = document.querySelectorAll(".item-checkbox:checked");
            console.log("Selected checkboxes found:", selectedCheckboxes.length);

            if (selectedCheckboxes.length === 0) {
                alert("Please select at least one item to delete.");
                return;
            }

            selectedCheckboxes.forEach(cb => {
                console.log("Checkbox value:", cb.value);
            });

            const ids = Array.from(selectedCheckboxes).map(cb => parseInt(cb.value)).filter(id => !isNaN(id));
            console.log("Parsed selected IDs:", ids);

            // document.getElementById("deleteItemId").value = ids[0];
            document.getElementById("deleteItemIds").value = ids.join(',');
        });
    }
    const selectAllCheckbox = document.getElementById("selectAllCheckbox");
    const itemCheckboxes = document.querySelectorAll(".item-checkbox");

    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener("change", function () {
            itemCheckboxes.forEach(cb => {
                cb.checked = selectAllCheckbox.checked;
            });
        });

        itemCheckboxes.forEach(cb => {
            cb.addEventListener("change", function () {
                if (!this.checked) {
                    selectAllCheckbox.checked = false;
                } else {
                    const allChecked = Array.from(itemCheckboxes).every(cb => cb.checked);
                    selectAllCheckbox.checked = allChecked;
                }
            });
        });
    }
});