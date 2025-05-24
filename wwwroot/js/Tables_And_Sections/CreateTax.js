document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("addTaxForm");

    form.addEventListener("submit", function () {
        const isEnabledCheckbox = document.getElementById("createTaxIsEnabled");
        const isDefaultCheckbox = document.getElementById("createTaxIsDefault");

        console.log("IsEnabled Checked:", isEnabledCheckbox.checked);
        console.log("IsDefault Checked:", isDefaultCheckbox.checked);

        isEnabledCheckbox.value = isEnabledCheckbox.checked ? "true" : "false";
        isDefaultCheckbox.value = isDefaultCheckbox.checked ? "true" : "false";
    });
});
