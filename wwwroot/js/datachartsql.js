
var myChart;
var myChartBar;
$(document).ready(function () {
    dynamic();
    

    
    function createDropdown(optionsArray) {
        
        //const container = document.getElementById('dropdown-container');
        const select = document.getElementById('dropdown-container');

        optionsArray.forEach(optionText => {
           
            const option = document.createElement('option');
           
            option.textContent = optionText;
            option.value = optionText;
           
            select.appendChild(option);
        });
        
        //container.appendChild(select);

        
    }
    
    
    fetchData().done(function (response) {
        apidata = response;
        var tradecode = response.data.map(row => row.trade_Code);
        const uniquetc = [...new Set(tradecode)];
        createDropdown(uniquetc);
        
        
        createChart(response);
        createChartBar(response);
        
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error('Error fetching data:', textStatus, errorThrown);
    });

    
});

function createChart(data) {
    var ctx = document.getElementById('myChart').getContext('2d');
     myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: data.data.map(row => row.date),
            datasets: [{
                label: 'Stock Data',
                data: data.data.map(row => row.close),
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
function createChartBar(data) {
    var ctx = document.getElementById('myChartBar').getContext('2d');
    myChartBar = new Chart(ctx, {
        type: 'bar',
        data: {
            datasets: [{
                label: 'Volume',
                data: data.data.map(row => row.volume),
                borderColor: "green",
                borderWidth: 2
            }, {
                label: 'Close',
                data: data.data.map(row => row.close),
                type: 'line',
                fill: true,
                lineTension: 0.1,

            }],//end data sets 
            labels: data.data.map(row => row.date)
        }//end data             
    });
}
function separateByName(arr,tc) {
    const result = {};
    const td = arr.data;
    result["data"] = [];
    td.forEach(item => {
        if (item.trade_Code == tc) {
            console.log(item.trade_Code);
            result["data"].push(item);
        }
        
    });

    return result;
}
function fetchData() {
    return $.ajax({
        url: '/Sql/GetAll',
        method: 'GET',
        dataType: 'json'
    });
}
function dynamic() {
    fetchData().done(function (response) {

        const selectElement = document.getElementById('dropdown-container');
        var selectedValue;
        selectElement.addEventListener('change', function (event) {
            selectedValue = event.target.value;
            console.log(selectedValue);
            var separatedItems = separateByName(response, selectedValue);
            console.log(separatedItems);
            myChart.destroy();
            myChartBar.destroy();
            createChart(separatedItems);
            createChartBar(separatedItems);
        });
        
        //console.log(response);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error('Error fetching data:', textStatus, errorThrown);
    });
}
