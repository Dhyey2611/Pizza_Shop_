document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll(".edit-tax-btn").forEach(btn => {
            btn.addEventListener("click", function () {
                const taxId = this.getAttribute("data-id");

                fetch(`/SuperAdmin/GetTaxById?id=${taxId}`)
                    .then(response => response.json())
                    .then(data => {
                        console.log("Data received from server:", data);

                        document.getElementById("editTaxId").value = data.id;
                        document.getElementById("editTaxName").value = data.name;
                        document.getElementById("editTaxType").value = data.type;
                        document.getElementById("editTaxValue").value = data.value;
                        document.getElementById("editTaxIsEnabled").checked = data.is_enabled;
                        document.getElementById("editTaxIsDefault").checked = data.is_default;

                        const editModal = new bootstrap.Modal(document.getElementById("editTaxModal"));
                        editModal.show();
                    })
                    .catch(error => {
                        console.error("Error fetching tax data:", error);
                    });
            });
        });
        document.getElementById("editTaxForm").addEventListener("submit", function () {
        const isEnabledCheckbox = document.getElementById("editTaxIsEnabled");
        const isDefaultCheckbox = document.getElementById("editTaxIsDefault");
        
        isEnabledCheckbox.value = isEnabledCheckbox.checked ? "true" : "false";
        isDefaultCheckbox.value = isDefaultCheckbox.checked ? "true" : "false";

    });
});