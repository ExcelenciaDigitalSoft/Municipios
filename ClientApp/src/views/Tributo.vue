<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Tributos</h2>
        <form @submit.prevent="crearTributo" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.descripcion" placeholder="Descripción" class="input" />
          <input v-model="nuevo.periodo" placeholder="Periodo AAAAMM" class="input" />
          <input v-model="nuevo.monto" type="number" placeholder="Monto" class="input" />
          <input v-model="nuevo.fechaEmision" type="date" placeholder="Fecha Emisión" class="input" />
          <input v-model="nuevo.fechaVencimiento" type="date" placeholder="Fecha Vencimiento" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="t in tributos" :key="t.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ t.descripcion }} - {{ t.periodo }} - ${{ t.monto }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(t)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarTributo(t.id)">Eliminar</button>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </template>
  
  <script>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  
  export default {
    setup() {
      const tributos = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        descripcion: '',
        periodo: '',
        monto: 0,
        fechaEmision: '',
        fechaVencimiento: '',
        idMunicipio: 1,
        idInmueble: 1,
        idTipoImpuesto: 1,
        idEstado: 1
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarTributos = async () => {
        const res = await axios.get('/api/admin/gestion/tributos', { headers });
        tributos.value = res.data;
      };
  
      const crearTributo = async () => {
        if (editar.value) {
          await axios.put(`/api/tributos/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/tributos', nuevo.value, { headers });
        }
        await cargarTributos();
        limpiarFormulario();
      };
  
      const eliminarTributo = async (id) => {
        await axios.delete(`/api/admin/gestion/tributos/${id}`, { headers });
        await cargarTributos();
      };
  
      const seleccionarEditar = (t) => {
        Object.assign(nuevo.value, { ...t });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          descripcion: '',
          periodo: '',
          monto: 0,
          fechaEmision: '',
          fechaVencimiento: '',
          idMunicipio: 1,
          idInmueble: 1,
          idTipoImpuesto: 1,
          idEstado: 1
        });
        editar.value = false;
      };
  
      onMounted(() => cargarTributos());
  
      return {
        tributos,
        nuevo,
        editar,
        crearTributo,
        eliminarTributo,
        seleccionarEditar
      };
    }
  };
  </script>
  
  <style scoped>
  .input {
    padding: 0.5rem;
    border: 1px solid #ccc;
    border-radius: 0.5rem;
  }
  .btn {
    background: #2d6cdf;
    color: white;
    padding: 0.5rem;
    border-radius: 0.5rem;
    font-weight: bold;
  }
  .card {
    background: white;
    padding: 1rem;
    border-radius: 1rem;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  }
  </style>