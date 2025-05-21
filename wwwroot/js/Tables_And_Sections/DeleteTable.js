document.addEventListener("DOMContentLoaded", function () {
    // Single delete (icon next to edit)
    document.querySelectorAll("[data-bs-target='#deleteTableModal']").forEach(btn => {
        btn.addEventListener("click", function () {
            const tableId = parseInt(this.getAttribute("data-id"));
            console.log("Single delete clicked - tableId:", tableId);
            document.getElementById("deleteTableIds").value = tableId;
        });
    });

    // Multi-delete (top trash icon)
    const topDeleteButton = document.querySelector(".btn-outline-secondary[data-bs-target='#deleteTableModal']");
    if (topDeleteButton) {
        topDeleteButton.addEventListener("click", function (e) {
            e.preventDefault();

            const selectedCheckboxes = document.querySelectorAll(".item-checkbox:checked");
            if (selectedCheckboxes.length === 0) {
                alert("Please select at least one table to delete.");
                return;
            }

            const ids = Array.from(selectedCheckboxes).map(cb => parseInt(cb.value));
            console.log("Multi delete selected table IDs:", ids);
            document.getElementById("deleteTableIds").value = ids.join(',');
        });
    }

    // Select/Deselect all checkboxes
    const selectAllCheckbox = document.getElementById("selectAllCheckbox");
    const tableCheckboxes = document.querySelectorAll(".item-checkbox");

    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener("change", function () {
            tableCheckboxes.forEach(cb => cb.checked = selectAllCheckbox.checked);
        });

        tableCheckboxes.forEach(cb => {
            cb.addEventListener("change", function () {
                if (!this.checked) {
                    selectAllCheckbox.checked = false;
                } else {
                    const allChecked = Array.from(tableCheckboxes).every(cb => cb.checked);
                    selectAllCheckbox.checked = allChecked;
                }
            });
        });
    }
});
