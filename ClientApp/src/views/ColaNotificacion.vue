<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Cola de Notificaciones</h2>
        <form @submit.prevent="crearCola" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.idPlantilla" placeholder="ID Plantilla" class="input" type="number" />
          <input v-model="nuevo.idContribuyente" placeholder="ID Contribuyente" class="input" type="number" />
          <input v-model="nuevo.celularDestino" placeholder="Celular Destino" class="input" />
          <select v-model="nuevo.canal" class="input">
            <option value="WhatsApp">WhatsApp</option>
            <option value="SMS">SMS</option>
            <option value="Email">Email</option>
          </select>
          <input v-model="nuevo.fechaProgramada" type="datetime-local" class="input" />
          <select v-model="nuevo.estadoEnvio" class="input">
            <option value="pendiente">Pendiente</option>
            <option value="enviado">Enviado</option>
            <option value="fallido">Fallido</option>
          </select>
          <input v-model="nuevo.intento" type="number" placeholder="Intento" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Programar' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="c in cola" :key="c.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ c.canal }} - {{ c.celularDestino }} - {{ c.estadoEnvio }} ({{ c.fechaProgramada }})</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(c)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarCola(c.id)">Eliminar</button>
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
      const cola = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        idPlantilla: null,
        idContribuyente: null,
        celularDestino: '',
        canal: 'WhatsApp',
        fechaProgramada: '',
        estadoEnvio: 'pendiente',
        intento: 0
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarCola = async () => {
        const res = await axios.get('/api/admin/notificaciones/cola', { headers });
        cola.value = res.data;
      };
  
      const crearCola = async () => {
        if (editar.value) {
          await axios.put(`/api/colanotificacion/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/notificaciones/cola', nuevo.value, { headers });
        }
        await cargarCola();
        limpiarFormulario();
      };
  
      const eliminarCola = async (id) => {
        await axios.delete(`/api/admin/notificaciones/cola/${id}`, { headers });
        await cargarCola();
      };
  
      const seleccionarEditar = (c) => {
        Object.assign(nuevo.value, { ...c });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          idPlantilla: null,
          idContribuyente: null,
          celularDestino: '',
          canal: 'WhatsApp',
          fechaProgramada: '',
          estadoEnvio: 'pendiente',
          intento: 0
        });
        editar.value = false;
      };
  
      onMounted(() => cargarCola());
  
      return {
        cola,
        nuevo,
        editar,
        crearCola,
        eliminarCola,
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