document.addEventListener("DOMContentLoaded", function () {
    const popupModal = document.querySelector("#selectModifiersModal"); // your popup modal
    const saveButton = popupModal.querySelector("#saveSelectedModifiersBtn"); // button inside modal to confirm

    saveButton.addEventListener("click", function () {
        // Collect checked modifier IDs
        const selectedIds = Array.from(popupModal.querySelectorAll(".modifier-checkbox:checked"))
            .map(cb => cb.value);

        // Send back to hidden input in main Add Modifier Group form
        const hiddenInput = document.querySelector("#selectedModifierIds");
        if (hiddenInput) {
            hiddenInput.value = selectedIds.join(",");
        }

        // Optionally close the modal after selection
        const modal = bootstrap.Modal.getInstance(popupModal);
        if (modal) modal.hide();
    });
});

