// Patrón Module para evitar variables globales
const ChartApp = (() => {
    // Configuración
    const config = {
        margin: { top: 30, right: 100, bottom: 50, left: 150 },
        baseWidth: 800,
        baseHeight: 500,
        barHeight: 25,
        barSpacing: 10,
        colorPalette: ['#3498db', '#2ecc71', '#e74c3c', '#f39c12', '#9b59b6'],
        animationDuration: 500
    };

    // Estado de la aplicación
    let state = {
        data: [],
        svg: null,
        isValid: false,
        currentWidth: config.baseWidth,
        currentHeight: config.baseHeight
    };

    // Elementos DOM
    const elements = {
        dataInput: document.getElementById('data-input'),
        updateBtn: document.getElementById('update-btn'),
        errorMsg: document.getElementById('error-msg'),
        statusMsg: document.getElementById('status-msg'),
        dataCount: document.getElementById('data-count'),
        chartContainer: document.getElementById('chart-container'),
        noDataMsg: document.getElementById('no-data-msg'),
        loading: document.getElementById('loading'),
        scrollHint: document.getElementById('scroll-hint')
    };

    // Tooltip
    const tooltip = d3.select("body")
        .append("div")
        .attr("class", "tooltip");

    // Inicializar la aplicación
    function init() {
        bindEvents();
        validateInput({ target: elements.dataInput });
    }

    // Vincular eventos
    function bindEvents() {
        elements.dataInput.addEventListener("input", validateInput);
        elements.updateBtn.addEventListener("click", updateChart);
        elements.dataInput.addEventListener("keypress", (e) => {
            if (e.key === "Enter") updateChart();
        });

        // Ajustar el tamaño del SVG cuando se redimensiona la ventana
        window.addEventListener('resize', debounce(() => {
            if (state.data.length > 0) {
                renderChart(state.data);
            }
        }, 250));
    }

    // Función debounce para mejorar el rendimiento
    function debounce(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }

    // Validar entrada de datos
    function validateInput(event) {
        const value = event.target.value;
        const validation = validateData(value);

        if (!validation.isValid) {
            showError(validation.message);
            return;
        }

        clearError();
        showStatus("Entrada válida", "valid");
    }

    // Validar datos
    function validateData(inputString) {
        if (!inputString.trim()) {
            return { isValid: false, message: "Por favor ingresa algunos valores" };
        }

        // ✅ Verificar que solo contenga números y comas
        if (!/^[0-9,]+$/.test(inputString)) {
            return { isValid: false, message: "Solo se permiten números y comas" };
        }

        if (inputString.endsWith(',')) {
            return { isValid: false, message: "Por favor agrega un número después de la última coma" };
        }

        const values = inputString.split(',').map(item => item.trim());
        const validNumbers = [];

        for (let i = 0; i < values.length; i++) {
            if (values[i] === "") {
                return {
                    isValid: false,
                    message: `Hay una coma sin número en la posición ${i + 1}. Por favor agrega un número.`
                };
            }

            // 🔢 Aseguramos que sea un número entero válido
            const num = Number(values[i]);
            if (!Number.isInteger(num)) {
                return {
                    isValid: false,
                    message: `"${values[i]}" no es un número válido en la posición ${i + 1}`
                };
            }
            validNumbers.push(num);
        }

        return { isValid: true, data: validNumbers, message: "Entrada válida" };
    }


    // Mostrar error
    function showError(message) {
        elements.errorMsg.textContent = message;
        elements.errorMsg.classList.add('show');
        elements.statusMsg.textContent = "Error en los datos";
        elements.statusMsg.className = "status invalid";
        elements.dataInput.classList.add('invalid');
        state.isValid = false;
    }

    // Limpiar error
    function clearError() {
        elements.errorMsg.classList.remove('show');
        elements.dataInput.classList.remove('invalid');
        state.isValid = true;
    }

    // Mostrar estado
    function showStatus(message, type) {
        elements.statusMsg.textContent = message;
        elements.statusMsg.className = `status ${type}`;
    }

    // Mostrar carga
    function showLoading() {
        elements.loading.style.display = 'block';
    }

    // Ocultar carga
    function hideLoading() {
        elements.loading.style.display = 'none';
    }

    // Actualizar gráfico
    function updateChart() {
        const inputValue = elements.dataInput.value;
        const validation = validateData(inputValue);

        if (!validation.isValid) {
            showError(validation.message);
            return;
        }

        clearError();
        showStatus("Procesando datos...", "valid");
        showLoading();

        // Usar setTimeout para permitir que la UI se actualice
        setTimeout(() => {
            try {
                renderChart(validation.data);
                elements.dataCount.textContent = `${validation.data.length} elementos mostrados`;
                showStatus("Gráfico actualizado correctamente", "valid");

                // Mostrar u ocultar la sugerencia de scroll según sea necesario
                const maxValue = d3.max(validation.data);
                const needsHorizontalScroll = maxValue > 100 || validation.data.length > 15;
                elements.scrollHint.style.display = needsHorizontalScroll ? 'block' : 'none';
            } catch (error) {
                showError(`Error al renderizar el gráfico: ${error.message}`);
            } finally {
                hideLoading();
            }
        }, 100);
    }

    // Renderizar gráfico
    function renderChart(data) {
        state.data = data;

        // Limpiar contenedor o inicializar SVG
        if (!state.svg) {
            initChart();
        } else {
            state.svg.selectAll("*").remove();
        }

        // Calcular dimensiones dinámicas
        const maxValue = d3.max(data);
        const chartWidth = Math.max(config.baseWidth, maxValue * 10 + config.margin.left + config.margin.right);
        const chartHeight = Math.max(config.baseHeight, data.length * (config.barHeight + config.barSpacing) + config.margin.top + config.margin.bottom);

        // Guardar dimensiones actuales
        state.currentWidth = chartWidth;
        state.currentHeight = chartHeight;

        // Actualizar dimensiones del SVG
        d3.select("#chart-container svg")
            .attr("width", chartWidth)
            .attr("height", chartHeight);

        // Crear escalas
        const yScale = d3.scaleBand()
            .domain(data.map((_, i) => i))
            .range([0, chartHeight - config.margin.top - config.margin.bottom])
            .padding(0.1);

        const xScale = d3.scaleLinear()
            .domain([0, maxValue])
            .range([0, chartWidth - config.margin.left - config.margin.right]);

        // Crear ejes
        const xAxis = d3.axisBottom(xScale);

        const yAxis = d3.axisLeft(yScale)
            .tickFormat(i => `Dato ${+i + 1}`);

        // Añadir ejes
        state.svg.append("g")
            .attr("class", "axis x-axis")
            .attr("transform", `translate(0,${chartHeight - config.margin.top - config.margin.bottom})`)
            .call(xAxis);

        state.svg.append("g")
            .attr("class", "axis y-axis")
            .call(yAxis);

        // Añadir etiquetas
        addAxisLabels(chartWidth, chartHeight);

        // Crear barras
        createBars(data, xScale, yScale, chartHeight);

        // Ocultar mensaje de no datos
        elements.noDataMsg.style.display = "none";
    }

    // Inicializar gráfico
    function initChart() {
        d3.select("#chart-container").selectAll("*").remove();

        state.svg = d3.select("#chart-container")
            .append("svg")
            .attr("width", state.currentWidth)
            .attr("height", state.currentHeight)
            .append("g")
            .attr("transform", `translate(${config.margin.left},${config.margin.top})`);
    }

    // Añadir etiquetas a los ejes
    function addAxisLabels(chartWidth, chartHeight) {
        state.svg.append("text")
            .attr("transform", `translate(${(chartWidth - config.margin.left - config.margin.right) / 2}, ${chartHeight - config.margin.top - config.margin.bottom + 40})`)
            .style("text-anchor", "middle")
            .text("Valor");

        state.svg.append("text")
            .attr("transform", "rotate(-90)")
            .attr("y", 0 - config.margin.left + 20)
            .attr("x", 0 - ((chartHeight - config.margin.top - config.margin.bottom) / 2))
            .attr("dy", "1em")
            .style("text-anchor", "middle")
            .text("Índice de Datos");
    }

    // Crear barras del gráfico
    function createBars(data, xScale, yScale, chartHeight) {
        const bars = state.svg.selectAll(".bar")
            .data(data)
            .enter()
            .append("g")
            .attr("class", "bar-group");

        // Añadir rectángulos de barras
        bars.append("rect")
            .attr("class", "bar")
            .attr("y", (_, i) => yScale(i))
            .attr("x", 0)
            .attr("height", yScale.bandwidth())
            .attr("width", d => xScale(d))
            .attr("fill", (_, i) => config.colorPalette[i % config.colorPalette.length])
            .on("mouseover", function (event, d) {
                handleBarHover(this, event, d);
            })
            .on("mouseout", handleBarLeave);

        // Añadir etiquetas de valor
        bars.append("text")
            .attr("class", "bar-label")
            .attr("y", (_, i) => yScale(i) + yScale.bandwidth() / 2)
            .attr("x", d => xScale(d) + 5)
            .text(d => d);
    }

    // Manejar hover sobre barras
    function handleBarHover(barElement, event, value) {
        d3.select(barElement)
            .attr("stroke", "#2c3e50")
            .attr("stroke-width", 2);

        tooltip.style("opacity", 1)
            .html(`Valor: ${value}<br>Posición: ${state.data.indexOf(value) + 1}`)
            .style("left", (event.pageX + 10) + "px")
            .style("top", (event.pageY - 25) + "px");
    }

    // Manejar salida del hover
    function handleBarLeave() {
        d3.select(this)
            .attr("stroke", null);

        tooltip.style("opacity", 0);
    }

    // API pública
    return {
        init
    };
})();

// Inicializar la aplicación cuando el DOM esté listo
document.addEventListener("DOMContentLoaded", ChartApp.init);