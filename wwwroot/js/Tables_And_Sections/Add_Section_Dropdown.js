
document.addEventListener("DOMContentLoaded", function () {
    const dropdown = document.getElementById("addTableSectionDropdown");
    const hiddenInput = document.getElementById("addTableSectionId");

    dropdown.addEventListener("change", function () {
        const selected = dropdown.options[dropdown.selectedIndex];
        hiddenInput.value = selected.getAttribute("data-id");
    });
});

