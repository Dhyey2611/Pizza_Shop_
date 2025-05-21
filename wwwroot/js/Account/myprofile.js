console.log("✅ JavaScript file is loaded in MyProfile.cshtml.");
window.onload = function () {
    console.log("✅ JavaScript Loaded: DOM Ready, Fetching Countries...");
    fetchCountries(); 

   
    selectedCountryId = document.getElementById("selectedCountryId")?.value;
    selectedStateId = document.getElementById("selectedStateId")?.value;
    selectedCityId = document.getElementById("selectedCityId")?.value;

    let countryDropdown = document.getElementById("countryDropdown");
    let stateDropdown = document.getElementById("stateDropdown");
    let cityDropdown = document.getElementById("cityDropdown");

    let hiddenCountryId = document.getElementById("hiddenCountryId");           
    let hiddenStateId = document.getElementById("hiddenStateId");
    let hiddenCityId = document.getElementById("hiddenCityId");


    if (selectedCountryId) {
        fetchStates(selectedCountryId)
    }
    if (selectedStateId) {
        fetchCities(selectedStateId)
    }


    if (countryDropdown) {
        console.log("✅ countryDropdown found in DOM.");
        countryDropdown.addEventListener("change", function () {
            console.log(`🔹 Country selected: ${this.value}, calling fetchStates()`);
            fetchStates(this.value);
            stateDropdown.innerHTML = `<option value="">Select State</option>`; 
            cityDropdown.innerHTML = `<option value="">Select City</option>`;  
            if (hiddenCountryId) hiddenCountryId.value = this.value;
        });
    }
    else {
        console.error("❌ countryDropdown NOT found in DOM.");
    }

    if (stateDropdown) {
        console.log("✅ State dropdown found.");
        stateDropdown.addEventListener("change", function () {
            console.log(`🔹 State selected: ${this.value}`);
            fetchCities(this.value);
            cityDropdown.innerHTML = `<option value="">Select City</option>`;
             if (hiddenStateId) hiddenStateId.value = this.value;
             if (hiddenCityId) hiddenCityId.value = this.value;
        });
    } else {
        console.error("❌ stateDropdown NOT found in DOM.");
    }
}

function fetchCountries() {
    console.log("🔹 Fetching countries...");
    fetch("/Account/GetCountries")
        .then(response => response.json())
        .then(data => {
            console.log(`✅ Received ${data.length} countries from API.`);
            let countryDropdown = document.getElementById("countryDropdown");
            if (!countryDropdown) {
                console.error("❌ countryDropdown not found.");
                return; 
            }
            countryDropdown.innerHTML = `<option value="">Select Country</option>`;
            data.forEach(country => {
                countryDropdown.innerHTML += `<option value="${country.id}">${country.name}</option>`;
            });
            console.log("✅ Countries updated in dropdown.");
            if (selectedCountryId) {
                countryDropdown.value = selectedCountryId;
                console.log(`➡️ Pre-selecting country ID: ${selectedCountryId} and fetching states...`);
                fetchStates(selectedCountryId);
            }
        })
        .catch(error => console.error("❌ Error fetching countries:", error));
}

function fetchStates(countryId) {
    console.log(`🔹 Fetching states for country: ${countryId}`);
    fetch(`/Account/GetStates?countryId=${countryId}`)
        .then(response => response.json())
        .then(data => {
            console.log(`✅ Received ${data.length} states from API.`);
            let stateDropdown = document.getElementById("stateDropdown");
            if (!stateDropdown) {
                console.error("❌ stateDropdown not found.");
                return; 
            }
            stateDropdown.innerHTML = `<option value="">Select State</option>`;
                data.forEach(state => {
                stateDropdown.innerHTML += `<option value="${state.id}">${state.name}</option>`;
            });
            console.log("✅ States updated in dropdown.");
            if (selectedStateId) {
                stateDropdown.value = selectedStateId;
                fetchCities(selectedStateId); 
            }
        })
        .catch(error => console.error("Error fetching states:", error));
}

function fetchCities(stateId) {
    let countryDropdown = document.getElementById("countryDropdown");
    if (!countryDropdown) {
        console.error("❌ countryDropdown not found.");
        return; 
    }
    let country = countryDropdown.value;
    console.log(`🔹 Fetching cities for country: ${country}, state: ${stateId}`);
    fetch(`/Account/GetCities?stateId=${stateId}`)
        .then(response => response.json())
        .then(data => {
            console.log(`✅ Received ${data.length} cities from API.`);
            let cityDropdown = document.getElementById("cityDropdown");
            if (!cityDropdown) {
                console.error("❌ cityDropdown not found.");
                return; 
            }
            cityDropdown.innerHTML = `<option value="">Select City</option>`;
            data.forEach(city => {
                cityDropdown.innerHTML += `<option value="${city.id}">${city.name}</option>`;
            });
            console.log("✅ Cities updated in dropdown.");
            if (selectedCityId) {
                cityDropdown.value = selectedCityId;
                console.log(`➡️ Pre-selecting city ID: ${selectedCityId}`);
            }
        })
    .catch(error => console.error("Error fetching cities:", error));
}