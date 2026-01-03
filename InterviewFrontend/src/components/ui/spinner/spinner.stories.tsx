import type { Meta, StoryObj } from '@storybook/react'
import { Spinner } from './spinner'

const meta = {
  title: 'UI/Spinner',
  component: Spinner,
  parameters: {
    layout: 'centered',
  },
  tags: ['autodocs'],
} satisfies Meta<typeof Spinner>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {},
}

export const Small: Story = {
  args: {
    className: 'size-3',
  },
}

export const Medium: Story = {
  args: {
    className: 'size-6',
  },
}

export const Large: Story = {
  args: {
    className: 'size-8',
  },
}

export const ExtraLarge: Story = {
  args: {
    className: 'size-12',
  },
}

export const CustomColor: Story = {
  args: {
    className: 'size-6 text-blue-500',
  },
}

export const InButton: Story = {
  render: () => (
    <button
      disabled
      className="inline-flex items-center gap-2 rounded-md bg-primary px-4 py-2 text-sm font-medium text-primary-foreground opacity-50"
    >
      <Spinner className="size-4" />
      Loading...
    </button>
  ),
}

export const InCard: Story = {
  render: () => (
    <div className="flex h-40 w-64 items-center justify-center rounded-lg border bg-card">
      <div className="flex flex-col items-center gap-3">
        <Spinner className="size-8" />
        <p className="text-sm text-muted-foreground">Loading content...</p>
      </div>
    </div>
  ),
}

export const Centered: Story = {
  render: () => (
    <div className="flex h-40 w-64 items-center justify-center">
      <Spinner className="size-6" />
    </div>
  ),
}

export const AllSizes: Story = {
  render: () => (
    <div className="flex items-center gap-6">
      <div className="flex flex-col items-center gap-2">
        <Spinner className="size-3" />
        <span className="text-xs text-muted-foreground">Extra Small</span>
      </div>
      <div className="flex flex-col items-center gap-2">
        <Spinner className="size-4" />
        <span className="text-xs text-muted-foreground">Small</span>
      </div>
      <div className="flex flex-col items-center gap-2">
        <Spinner className="size-6" />
        <span className="text-xs text-muted-foreground">Medium</span>
      </div>
      <div className="flex flex-col items-center gap-2">
        <Spinner className="size-8" />
        <span className="text-xs text-muted-foreground">Large</span>
      </div>
      <div className="flex flex-col items-center gap-2">
        <Spinner className="size-12" />
        <span className="text-xs text-muted-foreground">Extra Large</span>
      </div>
    </div>
  ),
}

export const WithText: Story = {
  render: () => (
    <div className="flex items-center gap-2">
      <Spinner className="size-4" />
      <span className="text-sm">Processing your request...</span>
    </div>
  ),
}
