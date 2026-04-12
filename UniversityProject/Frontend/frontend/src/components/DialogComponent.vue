<template>
  <el-dialog
      v-model="DeleteDialogVisibility"
      width="500"
      :before-close="close"
  >
    <span>{{props.questionString}}</span>
    <template #footer>
      <div class="dialog-footer">
        <el-button @click="cancel">Нет</el-button>
        <el-button type="primary" @click="confirm">Да</el-button>
      </div>
    </template>
  </el-dialog>
</template>

<style scoped>
  .dialog-footer
  {
    margin: 0 auto;
  }
</style>

<script setup lang="ts">
import {ref, watch} from "vue";
import type {TableColumn, TableData} from "@/types/TableTypes.ts";
  const props = defineProps <{
    modelValue : boolean;
    questionString: string;
  }> ();
  const DeleteDialogVisibility = ref(props.modelValue);
  const emit = defineEmits<
      {
        (e: 'update:modelValue', value: boolean): void
        (e: 'confirm'): void
        (e: 'cancel'): void
        (e: 'close'): void
      }>();
  const handleClose = (done: () => void) => { DeleteDialogVisibility.value = false;}
  watch(() => props.modelValue, (val) =>
  {
    DeleteDialogVisibility.value = val;
  })
  watch(DeleteDialogVisibility, (val) =>
  {
    emit('update:modelValue', val);
  })
  const cancel = () => {
    DeleteDialogVisibility.value = false
    emit('cancel');
  };
const close = (done : () => void) => {
  DeleteDialogVisibility.value = false
  emit('close');
  done();
};
  const confirm = () => {
    DeleteDialogVisibility.value = false
    emit("confirm");
  }
</script>